using AutoTeszt.Models.Opener;
using AutoTeszt.Models.Utilities;
using System;
using System.Management;
using System.Windows;

namespace AutoTeszt
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected readonly string m_user;

        public App()
        {
            m_user = Environment.UserName;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            ConsoleManager.Show();
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(new SelectQuery("Win32_UserAccount")))
            {
                var envVars = searcher.Get();
                foreach (ManagementObject envVar in envVars)
                {
                    Console.WriteLine(envVar["Name"].ToString());
                    if (envVar["Name"].ToString().ToLower() == "defaultuser0")
                    {
                        OsOpener.Instance.OpenSystem();
                        Current.Shutdown();
                        return;
                    }
                }
                OsOpener.Instance.RemoveStartup();
                base.OnStartup(e);
            }
        }
    }
}
