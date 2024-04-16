using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AutoTeszt.Models.Commands.Models
{
    internal class OpenSoundCommand : ATestCommand
    {
        public OpenSoundCommand() : base()
        {
            m_executionId = "sound";
        }
        public override void Execute(object prop)
        {
            if (prop is IEnumerable<string>)
            {
                Process.Start("control", $"mmsys.cpl,,{((IEnumerable<string>)prop).ElementAt(0)}");
            }
            else
            {
                Process.Start("control", $"mmsys.cpl,,0");
            }
        }
    }
}
