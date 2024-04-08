using AutoTeszt.Models.Blocks.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AutoTeszt.Models.Blocks.Managers
{
    internal class BlockManager
    {
        #region Variables
        private static ObservableCollection<Block> m_blocks = new ObservableCollection<Block>()
        {
            new AdminBlock(),
            new Block("Block 2", new SolidColorBrush(Colors.OrangeRed), "Block Decription 2"),
            new Block("Block 3", new SolidColorBrush(Colors.Violet), "Block Decription 3"),
            new Block("Block 4", new SolidColorBrush(Colors.GreenYellow), "Block Decription 4"),
            new Block("Block 5", new SolidColorBrush(Colors.LightGray), "Block Decription 5"),
            new Block("Block 6", new SolidColorBrush(Colors.SandyBrown), "Block Decription 6"),
            new Block("Block 7", new SolidColorBrush(Colors.SandyBrown), "Block Decription 6"),
            new Block("Block 8", new SolidColorBrush(Colors.SandyBrown), "Block Decription 6"),
            new Block("Block 9", new SolidColorBrush(Colors.SandyBrown), "Block Decription 6"),
            new Block("Block 10", new SolidColorBrush(Colors.SandyBrown), "Block Decription 6"),
        };
        #endregion
        #region Properties
        public static ObservableCollection<Block> Blocks => m_blocks;
        #endregion
    }
}
