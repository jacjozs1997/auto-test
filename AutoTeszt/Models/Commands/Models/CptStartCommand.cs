using System;
using System.Diagnostics;

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
            Console.WriteLine("Remove old networks");
            Process.Start(@"net use /delete /y v: && net use /delete /y z: && net use /delete /y o:");
            Console.WriteLine("Connecting networks");
            Process.Start(@"net use v: \\192.169.0.144\cptwur /u:readshare rs && net use z: \\10.222.245.225\images\bios /u:readshare rs && net use o: \\10.222.245.225\wur_bsod /u:readshare rs");
        }
    }
}
