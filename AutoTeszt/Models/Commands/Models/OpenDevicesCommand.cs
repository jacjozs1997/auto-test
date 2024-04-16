using System;
using System.Diagnostics;

namespace AutoTeszt.Models.Commands.Models
{
    internal class OpenDevicesCommand : ATestCommand
    {
        public OpenDevicesCommand() : base()
        {
            m_executionId = "device";
        }
        public override void Execute(object prop)
        {
            Console.WriteLine("Start device manager");
            Process.Start("devmgmt.msc");
        }
    }
}
