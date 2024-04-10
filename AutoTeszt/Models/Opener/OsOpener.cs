using AutoTeszt.Models.Utilities;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Management;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace AutoTeszt.Models.Opener
{
    public class OsOpener : Singleton<OsOpener>
    {
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        protected readonly string m_appName;
        protected readonly string m_appPath;

        public OsOpener()
        {
            var assembly = Assembly.GetExecutingAssembly();
            m_appName = assembly.GetName().Name;
            m_appPath = assembly.Location;
        }
        public void AddStartup()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                if (key.GetValue(m_appName) == null)
                    key.SetValue(m_appName, m_appPath);
            }
        }
        public void RemoveStartup()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                if (key.GetValue(m_appName) != null)
                    key.DeleteValue(m_appName, false);
            }
        }
        public void OpenSystem()
        {
            ConsoleManager.Show();
            bool loop = true;
            var userQuery = new SelectQuery("Win32_UserAccount");
            do
            {
                Process.Start("ms-cxh://SETADDNEWUSER");//Open login dialog
                Thread.Sleep(900);
                //IntPtr WindowToFind = FindWindow("ApplicationFrameWindow", null);
                //if (SetForegroundWindow(WindowToFind))
                //{
                SendKeys.SendWait("hp{enter}");
                //}
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(userQuery))
                {
                    var envVars = searcher.Get();
                    if (envVars.Count > 4)//Default+Admin+Defender+Guest
                    {
                        foreach (ManagementObject envVar in envVars)
                        {
                            if (envVar["Name"].ToString().ToLower() == "hp")
                            {
                                loop = false;
                                break;
                            }
                        }
                    }
                }
                if (loop)
                {
                    Process.Start("cmd.exe", "net user hp /DELETE");
                }
            } while (loop);
            Process.Start("regedit.exe", "/s DisablePrivacyExperience.reg").WaitForExit();//Setting oobe registry
            AddStartup();
            Process.Start("shutdown.exe", "-r -t 0");//Restart
        }
    }
}
