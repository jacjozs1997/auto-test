using AutoTeszt.Models.Commands.Services;
using System;

namespace AutoTeszt.Models.Commands.Models
{
    internal class HelpCommand : ATestCommand
    {
        public HelpCommand() : base()
        {
            m_executionId = "help";
        }
        public override void Execute(object prop)
        {
            Console.WriteLine("Supported commands:");
            var commands = CommandService.Instance.GetCommands();
            foreach (var command in commands)
            {
                if (command.ExecutionId != ExecutionId)
                {
                    Console.WriteLine($"\t{command.ExecutionId}\t- {command.Help}");
                }
            }
        }
    }
}
