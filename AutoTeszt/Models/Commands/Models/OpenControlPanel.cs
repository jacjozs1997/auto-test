using System;
using System.Diagnostics;

namespace AutoTeszt.Models.Commands.Models
{
    internal class OpenControlPanel : ATestCommand
    {
        public OpenControlPanel() : base()
        {
            m_executionId = "control";
        }
        public override void Execute(object prop)
        {
            Console.WriteLine("Start control panel");
            Process.Start("control");
        }
    }
}
