using System.Diagnostics;

namespace AutoTeszt.Models.Commands.Models
{
    internal class OpenDevicesCommand : ATestCommand
    {
        public OpenDevicesCommand() : base()
        {
            m_executionId = "device";
        }
        public override void Execute(object prop)
        {
            Process.Start("devmgmt.msc");
        }
    }
}
