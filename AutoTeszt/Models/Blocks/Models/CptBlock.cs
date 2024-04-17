using AutoTeszt.Models.Actions.Models.CPT;
using System.Windows.Media;

namespace AutoTeszt.Models.Blocks.Models
{
    internal class CptBlock : Block
    {
        public CptBlock() : base("CPT", new SolidColorBrush(Colors.Red), "CPT test folyamat")
        {
            this.m_actions.Add(new CptStart());
        }
    }
}
