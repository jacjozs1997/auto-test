using AutoTeszt.Models.Actions.Models.CPT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AutoTeszt.Models.Blocks.Models
{
    internal class CptBlock : Block
    {
        public CptBlock() : base("CPT", new SolidColorBrush(Colors.Red), "CPT indítása")
        {
            this.m_actions.Add(new CptStart());
        }
    }
}
