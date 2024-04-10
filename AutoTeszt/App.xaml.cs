using AutoTeszt.Models.Opener;
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
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(new SelectQuery("Win32_UserAccount")))
            {
                var envVars = searcher.Get();
                if (envVars.Count == 4)//Default+Admin+Defender+Guest
                {
                    OsOpener.Instance.OpenSystem();
                    Current.Shutdown();
                }
                else
                {
                    OsOpener.Instance.RemoveStartup();
                    base.OnStartup(e);
                }
            }
        }
    }
}
