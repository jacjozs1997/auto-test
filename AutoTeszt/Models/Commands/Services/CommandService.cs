using AutoTeszt.Models.Commands.Models;
using AutoTeszt.Models.Utilities;
using HandyControl.Tools.Command;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AutoTeszt.Models.Commands.Services
{
    public class CommandService : Singleton<CommandService>
    {
        #region Variables
        private ConcurrentDictionary<string, ATestCommand> m_commands = new ConcurrentDictionary<string, ATestCommand>();
        #endregion
        public CommandService()
        {
            var type = typeof(ATestCommand);

            var commands = Assembly.GetExecutingAssembly().GetTypes()
                 .Where(mytype => mytype.BaseType == type);

            ATestCommand command;
            foreach (Type commandType in commands)
            {
                command = Activator.CreateInstance(commandType) as ATestCommand;
                m_commands.TryAdd(command.ExecutionId.ToLower(), command);
            }
        }
        public RelayCommand<string> GetCommand(string executionId)
        {
            executionId = executionId.ToLower();
            if (executionId.Contains('|'))
            {
                var ids = executionId.Trim().Split('|');
                RelayCommand<string> command = null;
                foreach (var id in ids)
                {
                    command = GetSingeCommand(id);
                    if (command != null)
                    {
                        return command;
                    }
                }
                return null;
            } 
            else
            {
                return GetSingeCommand(executionId);
            }
        }
        private RelayCommand<string> GetSingeCommand(string id)
        {
            if (m_commands.ContainsKey(id))
            {
                return m_commands[id]?.Command ?? null;
            }
            return null;
        }
        public ICollection<ATestCommand> GetCommands()
        {
            return m_commands.Values;
        }
    }
}
