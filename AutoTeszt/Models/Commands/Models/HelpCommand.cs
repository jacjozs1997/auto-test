using AutoTeszt.Models.Commands.Services;
using AutoTeszt.Models.Terminal.Services;

namespace AutoTeszt.Models.Commands.Models
{
    internal class HelpCommand : ATestCommand
    {
        public HelpCommand() : base()
        {
            m_executionId = "help";
        }
        public override void Execute(string prop)
        {
            TerminalService.Instance.WriteLine("Supported commands:");
            var commands = CommandService.Instance.GetCommands();
            foreach (var command in commands)
            {
                if (command.ExecutionId != ExecutionId)
                {
                    TerminalService.Instance.WriteLine($"\t{command.ExecutionId}\t- {command.Help}");
                }
            }
        }
    }
}
