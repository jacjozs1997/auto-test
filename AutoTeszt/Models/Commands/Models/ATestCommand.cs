using HandyControl.Tools.Command;

namespace AutoTeszt.Models.Commands.Models
{
    public abstract class ATestCommand
    {
        #region Variables
        protected string m_executionId;
        protected RelayCommand<string> m_command;
        #endregion
        #region Properties
        public virtual string ExecutionId => m_executionId;
        public virtual RelayCommand<string> Command => m_command;
        public virtual string Help => null;
        #endregion

        public ATestCommand()
        {
            m_executionId = "clear|cls";
            m_command = new RelayCommand<string>((prop) => Execute(prop), (prop) => CanExecute(prop));
        }
        public virtual void Execute(string prop)
        {

        }
        public virtual bool CanExecute(string prop)
        {
            return true;
        }
    }
}
