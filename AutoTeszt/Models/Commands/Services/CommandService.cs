using AutoTeszt.Models.Commands.Models;
using AutoTeszt.Models.Utilities;
using HandyControl.Tools.Command;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AutoTeszt.Models.Commands.Services
{
    public class CommandService : Singleton<CommandService>
    {
        #region Variables
        private ConcurrentDictionary<string, ATestCommand> m_commands = new ConcurrentDictionary<string, ATestCommand>();
        private Task m_consoleListenerTask;
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
                if (command.ExecutionId.Contains('|'))
                {
                    var ids = command.ExecutionId.Trim().Split('|');
                    foreach (var id in ids)
                    {
                        m_commands.TryAdd(id, command);
                    }
                }
                else
                {
                    m_commands.TryAdd(command.ExecutionId.ToLower(), command);
                }
            }

            m_consoleListenerTask = new Task(() =>
            {
                string executionId = null;
                do
                {
                    executionId = Console.ReadLine();
                    var parts = executionId.Split(' ');
                    var commandS = parts[0];
                    var relayCommand = GetCommand(commandS);
                    if (relayCommand != null)
                    {
                        string props = null;
                        if (parts.Length > 1)
                            props = parts.Skip(1).Aggregate((s1, s2) => $"{s1} {s2}");
                        if (relayCommand.CanExecute(props))
                        {
                            relayCommand.Execute(props);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Try typing 'help' into the console and pressing enter.");
                    }
                } while (true);
            });
        }
        public RelayCommand<object> GetCommand(string id)
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
        public void StartConsoleListner()
        {
            m_consoleListenerTask?.Start();
        }
        public void StopConsoleListner()
        {
            m_consoleListenerTask?.Dispose();
        }
    }
}
