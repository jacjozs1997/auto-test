using AutoTeszt.Models.Actions.Models.StressTest;
using System.Windows.Media;

namespace AutoTeszt.Models.Blocks.Models
{
    internal class StressTestBlock : Block
    {
        public StressTestBlock() : base("Kombi", new SolidColorBrush(Colors.DarkOrange), "CPU és GPU terhelés")
        {
            this.m_actions.Add(new Combi());
            this.m_actions.Add(new Furmark());
            this.m_actions.Add(new Cpuburner());
        }
    }
}
