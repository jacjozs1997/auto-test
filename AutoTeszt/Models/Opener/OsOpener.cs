using AutoTeszt.Models.Utilities;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Globalization;
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
        protected readonly string m_hpUserName;
        protected readonly SelectQuery m_userSelectQuery;

        public OsOpener()
        {
            var assembly = Assembly.GetExecutingAssembly();
            m_appName = assembly.GetName().Name;
            m_appPath = assembly.Location;
            m_hpUserName = "hp";
            m_userSelectQuery = new SelectQuery("Win32_UserAccount");
        }
        public void AddStartup()
        {
            Console.WriteLine("Add startup registry");
            using (RegistryKey key = Registry.Users.OpenSubKey($"{GetUserId(m_hpUserName)}\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                if (key != null && key.GetValue(m_appName) == null)
                    key.SetValue(m_appName, m_appPath);
                else
                    Console.Error.Write("Faild add startup registry");
            }
        }
        public void RemoveStartup()
        {
            Console.WriteLine("Remove startup registry");
            using (RegistryKey key = Registry.Users.OpenSubKey($"{GetUserId(m_hpUserName)}\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                if (key != null && key.GetValue(m_appName) != null)
                    key.DeleteValue(m_appName, false);
                else
                    Console.Error.Write("Faild remove startup registry");
            }
        }
        public void DisablePrivacyRegistry()
        {
            Console.WriteLine("Disable Privacy Experience");
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\OOBE", true))
            {
                if (key.GetValue("DisablePrivacyExperience") == null)
                    key.SetValue("DisablePrivacyExperience", 1);
                else
                    Console.Error.Write("Faild disable privacy experience");
            }
        }
        public void OpenSystem()
        {
            bool loop = true;
            int i = 0;
            do
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(m_userSelectQuery))
                {
                    var envVars = searcher.Get();
                    foreach (ManagementObject envVar in envVars)//Default+Admin+Defender+Guest
                    {
                        Console.WriteLine($"Found user: {envVar["Name"]}");
                        if (envVar["Name"].ToString().ToLower() == m_hpUserName)
                        {
                            Console.WriteLine($"Delete old {m_hpUserName} user");
                            ProcessStartInfo psi = new ProcessStartInfo();
                            psi.FileName = "cmd.exe";
                            psi.UseShellExecute = false;
                            psi.RedirectStandardError = true;
                            psi.RedirectStandardOutput = true;
                            psi.Arguments = $"net user {m_hpUserName} /DELETE";

                            Process proc = Process.Start(psi);
                            proc.WaitForExit();

                            string errorOutput = proc.StandardError.ReadToEnd();
                            string standardOutput = proc.StandardOutput.ReadToEnd();
                            if (proc.ExitCode != 0)
                                throw new Exception("cmd exit code: " + proc.ExitCode.ToString() + " " + (!string.IsNullOrEmpty(errorOutput) ? " " + errorOutput : "") + " " + (!string.IsNullOrEmpty(standardOutput) ? " " + standardOutput : ""));
                            break;
                        }
                    }
                    Console.WriteLine($"Open microsoft account window");
                    Process.Start("ms-cxh://SETADDNEWUSER");//Open login dialog
                    Thread.Sleep(900 + (100 * i++));
                    //IntPtr WindowToFind = FindWindow(null, GetMicrosoftTitle());
                    //if (SetForegroundWindow(WindowToFind))
                    //{
                    SendKeys.SendWait(m_hpUserName + "{enter}");
                    //}
                    envVars = searcher.Get();
                    foreach (ManagementObject envVar in envVars)//Default+Admin+Defender+Guest
                    {
                        if (envVar["Name"].ToString() == m_hpUserName)
                        {
                            Console.WriteLine($"Break user: {envVar["Name"]}");
                            loop = false;
                            break;
                        }
                    }
                }
            } while (loop);
            DisablePrivacyRegistry();
            //Process.Start("regedit.exe", "/s DisablePrivacyExperience.reg").WaitForExit();//Setting oobe registry
            AddStartup();
            Console.WriteLine("Restarting");
            Console.ReadLine();
            Process.Start("shutdown.exe", "-r -t 0");//Restart
        }
        string GetUserId(string name)
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(m_userSelectQuery))
            {
                var envVars = searcher.Get();
                foreach (ManagementObject envVar in envVars)
                {
                    if (envVar["Name"].ToString().ToLower() == name.ToLower())
                    {
                        return envVar["SID"] as string;
                    }
                }
            }
            return null;
        }
        string GetMicrosoftTitle()
        {
            string title = "";
            CultureInfo ci = CultureInfo.InstalledUICulture;
            switch (ci.TwoLetterISOLanguageName.ToLower())
            {
                case "hu":
                    title = "Microsoft-fiók";
                    break;              
                case "en":
                    title = "Microsoft-account";
                    break;
            }
            return title;
        }
    }
}
