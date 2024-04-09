using AutoTeszt.Models.Commands.Services;
using AutoTeszt.Models.Console.Services;
using HandyControl.Tools.Command;
using System;
using System.Linq;
using System.Management;
using System.Windows.Input;

namespace AutoTeszt.ViewModel
{
    class MainWindowViewModel
    {
        public string Title
        {
            get
            {
                string sn = null;
                string pn = null;
                string sku = null;
                string manufacturer = null;
                string model = null;
                string bios = null;

                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(new SelectQuery(@"SELECT Product, SerialNumber, SKU FROM Win32_BaseBoard")))
                {
                    foreach (ManagementObject process in searcher.Get())
                    {
                        Console.WriteLine("/*********Operating System Information ***************/");
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
                        if (((string[])process["BIOSVersion"]).Length > 1)
                            Console.WriteLine("BIOS VERSION: " + ((string[])process["BIOSVersion"])[0] + " - " + ((string[])process["BIOSVersion"])[1]);
                        else
                            Console.WriteLine("BIOS VERSION: " + ((string[])process["BIOSVersion"])[0]);

                        bios = ((string[])process["BIOSVersion"]).Length > 1 ? ((string[])process["BIOSVersion"])[0] + " - " + ((string[])process["BIOSVersion"])[1] : ((string[])process["BIOSVersion"])[0];
                    }
                }
                return $"Előteszt by DagadtNeo - Serial: {sn} - PN: {pn} - {(sku != null ? ("SKU: " + sku) : "")} - Manufact: {manufacturer} - Model: {model} - Bios Ver: {bios}";
            }
        }

        public MainWindowViewModel()
        {
            ExecuteCommand = new RelayCommand<string>(Execute);
        }

        public ICommand ExecuteCommand
        {
            get;
            private set;
        }

        private void Execute(string executionId)
        {
            var parts = executionId.Split(' ');
            var commandS = parts[0];
            var command = CommandService.Instance.GetCommand(commandS);
            if (command != null)
            {
                string props = null;
                if (parts.Length > 1)
                    props = parts.Skip(1).Aggregate((s1, s2) => $"{s1} {s2}");
                if (command.CanExecute(props))
                {
                    command.Execute(props);
                }
            } 
            else
            {
                ConsoleService.Instance.WriteLine($"Try typing 'help' into the console and pressing enter.");
            }
        }
    }
}
