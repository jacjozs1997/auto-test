using System;
using System.Diagnostics;

namespace AutoTeszt.Models.Commands.Models
{
    internal class PerfmonCommand : ATestCommand
    {
        public PerfmonCommand() : base()
        {
            m_executionId = "perfmon";
        }
        public override void Execute(object prop)
        {
            Console.WriteLine("Start perfmon");
            Process.Start("perfmon", "/rel");
        }
    }
}
