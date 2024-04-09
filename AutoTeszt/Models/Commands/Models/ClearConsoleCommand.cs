using AutoTeszt.Models.Console.Services;

namespace AutoTeszt.Models.Commands.Models
{
    internal class ClearConsoleCommand : ATestCommand
    {
        public ClearConsoleCommand() : base()
        {
            m_executionId = "clear|cls";
        }
        public override void Execute(string prop)
        {
            ConsoleService.Instance.Clear();
        }
    }
}
