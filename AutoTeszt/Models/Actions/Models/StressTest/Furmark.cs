using AutoTeszt.Models.Tests;
using System.Windows.Controls;

namespace AutoTeszt.Models.Actions.Models.StressTest
{
    internal class Furmark : BlockAction
    {
        public Furmark() : base("furmark", "Furmark")
        {
            ((Button)UIElement).Margin = new System.Windows.Thickness(0, 5, 0, 5);
        }
    }
}
