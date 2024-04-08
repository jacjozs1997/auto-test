using AutoTeszt.Models.Tests;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace AutoTeszt.Models.Blocks
{
    internal class Block
    {
        #region Variables
        private string m_name;
        private string m_description;
        private Brush m_color;
        protected ObservableCollection<BlockAction> m_actions = new ObservableCollection<BlockAction>();
        #endregion
        #region Properties
        public string Name => this.m_name;
        public string Description => this.m_description;
        public Brush Color => this.m_color;
        public ObservableCollection<BlockAction> Actions => this.m_actions;
        #endregion
        public Block(string name, SolidColorBrush color, string description = "")
        {
            this.m_name = name;
            this.m_description = description;
            this.m_color = color;
        }
    }
}
