using System;
using System.Windows;
using System.Windows.Input;
using AutoTeszt.Models.Blocks.Managers;
using AutoTeszt.Models.Console.Services;
using AutoTeszt.ViewModel;
using AutoTeszt.Views;
using HandyControl.Controls;

namespace AutoTeszt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            var userName = System.Security.Principal.WindowsIdentity.GetCurrent();
            var myCurrentLang = InputLanguageManager.Current.CurrentInputLanguage;
            InitializeComponent();
            DataContext = new MainWindowViewModel(ConsoleService.Instance);
            foreach (var block in BlockManager.Blocks)
            {
                var newBlock = new Block()
                {
                    DataContext = block
                };
                newBlock.Loaded += AddBlockActions;
                FlexPanel.Children.Add(newBlock);
            }
        }

        private void AddBlockActions(object sender, EventArgs e)
        {
            var view = sender as Block;
            var block = view.DataContext as Models.Blocks.Block;
            foreach (var action in block.Actions)
            {
                view.ActionContainer.Children.Add(action.UIElement);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Growl.InfoGlobal("hhgjfghhg");
        }
    }
}
