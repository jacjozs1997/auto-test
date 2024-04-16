using System;
using System.Diagnostics;

namespace AutoTeszt.Models.Commands.Models
{
    internal class EventsViwerCommand : ATestCommand
    {
        public EventsViwerCommand() : base()
        {
            m_executionId = "events";
        }
        public override void Execute(object prop)
        {
            Console.WriteLine("Start events viewer");
            Process.Start("eventvwr");
        }
    }
}
