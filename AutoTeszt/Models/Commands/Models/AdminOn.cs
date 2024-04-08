using AutoTeszt.Models.Console.Services;
using HandyControl.Tools.Command;

namespace AutoTeszt.Models.Commands.Models
{
    internal class AdminOn : ITestCommand
    {
        #region Variables
        private string m_executionId;
        private RelayCommand<string> m_command;
        #endregion
        #region Properties
        public string ExecutionId => m_executionId;
        public RelayCommand<string> Command => m_command;
        #endregion
        public AdminOn()
        {
            m_executionId = "AdminOn";
            m_command = new RelayCommand<string>((prop) => Execute(prop), (prop) => CanExecute(prop));
        }
        public void Execute(string prop)
        {
            ConsoleService.Instance.WriteLine($"{m_executionId}");
        }
        public bool CanExecute(string prop)
        {
            return true;
        }
    }
}
