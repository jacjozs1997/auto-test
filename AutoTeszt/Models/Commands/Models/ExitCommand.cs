namespace AutoTeszt.Models.Commands.Models
{
    internal class ExitCommand : ATestCommand
    {
        public ExitCommand() : base()
        {
            m_executionId = "exit|close";
        }
        public override void Execute(object prop)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
