using HandyControl.Tools.Command;

namespace AutoTeszt.Models.Commands.Models
{
    internal interface ITestCommand
    {
        string ExecutionId { get; }
        RelayCommand<string> Command { get; }
    }
}
