using AutoTeszt.Models.Commands.Services;
using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AutoTeszt.Models.Tests
{
    internal class BlockAction
    {
        #region Variables
        private string m_executionId;
        private string m_name;
        private UIElement m_uiElement = null;
        #endregion
        #region Properties
        public string Name => m_name;
        public string ExecutionId => m_executionId;
        public virtual UIElement UIElement
        {
            get
            {
                if (this.m_uiElement == null)
                {
                    this.m_uiElement = new Button()
                    {
                        Content = this.m_name,
                        FontWeight = FontWeights.Bold,
                        FontSize = 14,
                        FontFamily = new System.Windows.Media.FontFamily("Arial Rounded MT Bold")
                    };
                    ((Button)m_uiElement).Command = CommandService.Instance.GetCommand(this.m_executionId);
                }
                return this.m_uiElement;
            }
        }
        #endregion
        public BlockAction(string executionId, string name)
        {
            this.m_executionId = executionId;
            this.m_name = name;
        }

        public BlockAction()
        {
        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            Growl.InfoGlobal(this.Name);
        }
    }
}
