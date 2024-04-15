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
            Process.Start("control");
        }
    }
}
