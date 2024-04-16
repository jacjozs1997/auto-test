﻿using AutoTeszt.Models.Commands.Services;
using HandyControl.Tools.Command;
using System.Globalization;
using System.Linq;
using System.Management;

namespace AutoTeszt.ViewModel
{
    public class MainWindowViewModel
    {
        #region Variables
        private RelayCommand<string> m_selectCmd;
        #endregion
        public static string Title
        {
            get
            {
                string sn = null;
                string pn = null;
                string sku = null;
                string manufacturer = null;
                string model = null;
                string bios = null;
                CultureInfo ci = CultureInfo.InstalledUICulture;
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(new SelectQuery(@"SELECT Product, SerialNumber, SKU FROM Win32_BaseBoard")))
                {
                    foreach (ManagementObject process in searcher.Get())
                    {
                        sn = process["SerialNumber"] as string;
                        pn = process["Product"] as string;
                        sku = process["SKU"] as string;
                    }
                }
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(new SelectQuery(@"Select * from Win32_ComputerSystem")))
                {
                    foreach (ManagementObject process in searcher.Get())
                    {
                        process.Get();
                        manufacturer = process["Manufacturer"] as string;
                        model = process["Model"] as string;
                    }
                }
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(new SelectQuery(@"SELECT * FROM Win32_BIOS")))
                {
                    foreach (ManagementObject process in searcher.Get())
                    {
                        bios = ((string[])process["BIOSVersion"]).Length > 1 ? ((string[])process["BIOSVersion"])[0] + " - " + ((string[])process["BIOSVersion"])[1] : ((string[])process["BIOSVersion"])[0];
                    }
                }
                return $"Előteszt by DagadtNeo - Serial: {sn} - PN: {pn} {(sku != null ? (" - SKU: " + sku) : "")} - Manufact: {manufacturer} - Model: {model} - Bios Ver: {bios} - Language: {ci.TwoLetterISOLanguageName} - Keyboard layout id: {ci.KeyboardLayoutId}";
            }
        }
        public RelayCommand<string> SelectCmd => m_selectCmd;
        private void Select(string command)
        {
            var args = command.Split(',');
            var relayCommand = CommandService.Instance.GetCommand(args[0].ToLower());
            if (relayCommand != null && relayCommand.CanExecute(args.Skip(1)))
            {
                relayCommand.Execute(args.Skip(1));
            }
        }
        public MainWindowViewModel()
        {
            m_selectCmd = new RelayCommand<string>(Select, x => true);
        }
    }
}
