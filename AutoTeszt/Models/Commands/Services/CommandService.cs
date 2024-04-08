using AutoTeszt.Models.Commands.Models;
using AutoTeszt.Models.Utilities;
using HandyControl.Tools.Command;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace AutoTeszt.Models.Commands.Services
{
    public class CommandService : Singleton<CommandService>
    {
        #region Variables
        private ConcurrentDictionary<string, RelayCommand<string>> m_commands = new ConcurrentDictionary<string, RelayCommand<string>>();
        #endregion
        public CommandService()
        {
            var type = typeof(ITestCommand);

            var commands = Assembly.GetExecutingAssembly().GetTypes()
                 .Where(mytype => mytype.GetInterfaces().Contains(type));

            ITestCommand command;
            foreach (Type commandType in commands)
            {
                command = Activator.CreateInstance(commandType) as ITestCommand;
                m_commands.TryAdd(command.ExecutionId, command.Command);
            }
        }
        public RelayCommand<string> GetCommand(string executionId)
        {
            if (m_commands.ContainsKey(executionId))
            {
                return m_commands[executionId];
            }
            return null;
        }
    }
}
