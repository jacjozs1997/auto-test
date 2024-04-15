using AutoTeszt.Models.Commands.Services;
using AutoTeszt.Models.Opener;
using AutoTeszt.Models.Utilities;
using AutoTeszt.ViewModel;
using System;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
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
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            CultureInfo ci = CultureInfo.InstalledUICulture;
            ConsoleManager.Show();
            Console.Title = MainWindowViewModel.Title;
            Console.WriteLine($"Environment.UserName: {m_user}");
            Console.WriteLine($"Language: {ci.TwoLetterISOLanguageName}; Keyboard layout id: {ci.KeyboardLayoutId}");
            Console.Write("Open system 0:1?");
            string read = Console.ReadLine();
            if (read == "1")//Environment.UserName.ToLower() == "defaultuser0"
            {
                OsOpener.Instance.OpenSystem();
                Current.Shutdown();
                return;
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
