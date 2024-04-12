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
            if (Environment.UserName.ToLower() == "defaultuser0")
            {
                OsOpener.Instance.OpenSystem();
                Current.Shutdown();
                return;
            }
            OsOpener.Instance.RemoveStartup();
            base.OnStartup(e);
        }
    }
}
