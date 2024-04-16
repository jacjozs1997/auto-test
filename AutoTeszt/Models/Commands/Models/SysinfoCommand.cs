using System;
using System.Diagnostics;

namespace AutoTeszt.Models.Commands.Models
{
    internal class SysinfoCommand : ATestCommand
    {
        public SysinfoCommand() : base()
        {
            m_executionId = "sysinfo";
        }
        public override void Execute(object prop)
        {
            Console.WriteLine("Start system information");
            Process.Start("msinfo32");
        }
    }
}
