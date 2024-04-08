using AutoTeszt.Models.Actions.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AutoTeszt.Models.Blocks.Models
{
    internal class AdminBlock : Block
    {
        public AdminBlock() : base("Admin", new SolidColorBrush(Colors.Green), "Admin fiók nyitása-zárása")
        {
            this.m_actions.Add(new AdminOn());
            this.m_actions.Add(new AdminOff());
        }
    }
}
