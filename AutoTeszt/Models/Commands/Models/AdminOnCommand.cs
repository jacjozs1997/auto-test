using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTeszt.Models.Commands.Models
{
    internal class AdminOnCommand : ATestCommand
    {
        public AdminOnCommand() : base()
        {
            m_executionId = "AdminOn";
        }
        public override void Execute(string prop)
        {
            Console.WriteLine("AdminOn");
        }
    }
}
