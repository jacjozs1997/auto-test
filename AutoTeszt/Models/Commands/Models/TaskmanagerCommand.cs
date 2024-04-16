using System;
using System.Diagnostics;

namespace AutoTeszt.Models.Commands.Models
{
    internal class TaskmanagerCommand : ATestCommand
    {
        public TaskmanagerCommand() : base()
        {
            m_executionId = "taskmgr";
        }
        public override void Execute(object prop)
        {
            Console.WriteLine("Start task manager");
            Process.Start("taskmgr", "Performance");
        }
    }
}
