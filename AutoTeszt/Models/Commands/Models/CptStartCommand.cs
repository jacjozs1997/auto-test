using AutoTeszt.Models.Utilities;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace AutoTeszt.Models.Commands.Models
{
    internal class CptStartCommand : ATestCommand
    {
        public CptStartCommand() : base()
        {
            m_executionId = "cpt";
        }
        public override void Execute(object prop)
        {
            Console.WriteLine("Start CPT");
            #region Delete old networks
            Console.WriteLine("Disconnected networks...");
            DriveSettings.DisconnectNetworkDrive("V", true);
            DriveSettings.DisconnectNetworkDrive("Z", true);
            DriveSettings.DisconnectNetworkDrive("O", true);
            Console.WriteLine("Disconnected networks");
            #endregion
            #region Connection networks
            Console.WriteLine("Connecting networks...");
            DriveSettings.MapNetworkDrive("V", @"\\192.169.0.144\cptwur", "readshare", "rs");
            DriveSettings.MapNetworkDrive("Z", @"\\10.222.245.225\images\bios", "readshare", "rs");
            DriveSettings.MapNetworkDrive("O", @"\\10.222.245.225\wur_bsod", "readshare", "rs");
            Console.WriteLine("Connected networks");
            #endregion
            Process.Start(@"v:\cptwur\program\CPTLoader.exe").WaitForExit();

            if (Directory.Exists(@"C:\cpt"))
                Directory.Delete(@"C:\cpt");

            if (Directory.Exists(@"C:\oa3"))
                Directory.Delete(@"C:\oa3");
        }
    }
}
