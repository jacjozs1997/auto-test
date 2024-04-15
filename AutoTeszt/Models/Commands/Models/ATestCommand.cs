using HandyControl.Tools.Command;

namespace AutoTeszt.Models.Commands.Models
{
    public abstract class ATestCommand
    {
        #region Variables
        protected string m_executionId;
        protected RelayCommand<object> m_command;
        #endregion
        #region Properties
        public virtual string ExecutionId => m_executionId;
        public virtual RelayCommand<object> Command => m_command;
        public virtual string Help => null;
        #endregion

        public ATestCommand()
        {
            m_command = new RelayCommand<object>((prop) => Execute(prop), (prop) => CanExecute(prop));
        }
        public virtual void Execute(object prop)
        {

        }
        public virtual bool CanExecute(object prop)
        {
            return true;
        }
    }
}
