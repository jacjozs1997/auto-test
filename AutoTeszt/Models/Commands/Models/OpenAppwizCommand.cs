using System;
using System.Diagnostics;

namespace AutoTeszt.Models.Commands.Models
{
    internal class OpenAppwizCommand : ATestCommand
    {
        public OpenAppwizCommand() : base()
        {
            m_executionId = "appwiz";
        }
        public override void Execute(object prop)
        {
            Console.WriteLine("Start app wizard");
            Process.Start("appwiz.cpl");
        }
    }
}
