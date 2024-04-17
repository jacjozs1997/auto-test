using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTeszt.Models.Commands.Models
{
    internal class FurmarkCommand : ATestCommand
    {
        public FurmarkCommand() : base()
        {
            m_executionId = "help";
        }
        public override void Execute(object prop)
        {
            
        }
    }
}
