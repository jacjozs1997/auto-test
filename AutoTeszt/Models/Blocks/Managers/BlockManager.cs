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
            new CptBlock(),
            new StressTestBlock(),
            //new AdminBlock(),
        };
        #endregion
        #region Properties
        public static ObservableCollection<Block> Blocks => m_blocks;
        #endregion
    }
}
