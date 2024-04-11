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
            Console.WriteLine("Add startup registry");
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                if (key.GetValue(m_appName) == null)
                    key.SetValue(m_appName, m_appPath);
            }
        }
        public void RemoveStartup()
        {
            Console.WriteLine("Remove startup registry");
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                if (key.GetValue(m_appName) != null)
                    key.DeleteValue(m_appName, false);
            }
        }
        public void OpenSystem()
        {
            Console.WriteLine($"Environment.UserName: {Environment.UserName}");
            bool loop = true;
            var userQuery = new SelectQuery("Win32_UserAccount");
            do
            {
                Process.Start("ms-cxh://SETADDNEWUSER");//Open login dialog
                Thread.Sleep(900);
                //IntPtr WindowToFind = FindWindow("ApplicationFrameWindow", null);
                //if (SetForegroundWindow(WindowToFind))
                //{
                SendKeys.SendWait("hp{tab}");
                //}
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(userQuery))
                {
                    var envVars = searcher.Get();
                    foreach (ManagementObject envVar in envVars)//Default+Admin+Defender+Guest
                    {
                        Console.WriteLine($"Found user: {envVar["Name"]}");
                        if (envVar["Name"].ToString().ToLower() == "hp")
                        {
                            Console.WriteLine($"Break user: {envVar["Name"]}");
                            loop = false;
                            break;
                        }
                    }
                }
                if (loop)
                {
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = "cmd.exe";
                    psi.UseShellExecute = false;
                    psi.RedirectStandardError = true;
                    psi.RedirectStandardOutput = true;
                    psi.Arguments = "net user hp /DELETE";

                    Process proc = Process.Start(psi);
                    proc.WaitForExit();

                    string errorOutput = proc.StandardError.ReadToEnd();
                    string standardOutput = proc.StandardOutput.ReadToEnd();
                    if (proc.ExitCode != 0)
                        throw new Exception("cmd exit code: " + proc.ExitCode.ToString() + " " + (!string.IsNullOrEmpty(errorOutput) ? " " + errorOutput : "") + " " + (!string.IsNullOrEmpty(standardOutput) ? " " + standardOutput : ""));
                }
            } while (loop);
            Console.WriteLine("Disable Privacy Experience");
            Process.Start("regedit.exe", "/s DisablePrivacyExperience.reg").WaitForExit();//Setting oobe registry
            AddStartup();
            Console.WriteLine("Restart");
            Process.Start("shutdown.exe", "-r -t 0");//Restart
        }
    }
}
