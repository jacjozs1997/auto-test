using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Process.Start("control", $"mmsys.cpl,,1");
        }
    }
}
