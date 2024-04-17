using AutoTeszt.Models.Opener;
using AutoTeszt.Models.Utilities;
using AutoTeszt.ViewModel;
using JsonConfig;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;

namespace AutoTeszt
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected readonly string m_user;

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public App()
        {
            m_user = Environment.UserName;
            using (var reader = new StreamReader(@"config.json"))
            {
                Config.Default = Config.ApplyJson(reader.ReadToEnd(), new ConfigObject());
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            if (Config.Default.Console || Environment.UserName.ToLower() == "defaultuser0")
            {
                ConsoleManager.Show();
                Console.Title = MainWindowViewModel.Title;

                if (Environment.UserName.ToLower() == "defaultuser0")
                {
                    OsOpener.Instance.OpenSystem();
                    Current.Shutdown();
                    return;
                }
            }
            OsOpener.Instance.RemoveStartup();
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            ConsoleManager.Hide();
        }
    }
}
