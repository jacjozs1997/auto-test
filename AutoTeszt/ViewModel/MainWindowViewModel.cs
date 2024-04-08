using AutoTeszt.Models.Commands.Services;
using AutoTeszt.Models.Console.Services;
using HandyControl.Tools.Command;
using System;
using System.Linq;
using System.Windows.Input;

namespace AutoTeszt.ViewModel
{
    class MainWindowViewModel
    {
        readonly ConsoleService m_consoleService;

        public MainWindowViewModel(ConsoleService consoleService)
        {
            m_consoleService = consoleService;

            ExecuteCommand = new RelayCommand<string>(Execute);
        }

        public ICommand ExecuteCommand
        {
            get;
            private set;
        }

        private void Execute(string executionId)
        {
            var parts = executionId.Split(' ');
            var command = parts[0];
            CommandService.Instance.GetCommand(command)?.Execute(null);
            switch (command)
            {
                case "echo":
                    m_consoleService.WriteLine(parts.Skip(1).Aggregate((s1, s2) => s1 + " " + s2));
                    break;

                case "time":
                    m_consoleService.WriteLine(DateTime.Now.ToLongTimeString());
                    break;

                case "date":
                    m_consoleService.WriteLine(DateTime.Now.ToLongDateString());
                    break;

                case "cls":
                case "clear":
                    m_consoleService.Clear();
                    break;

                case "help":
                    m_consoleService.WriteLine("Supported commands:");
                    m_consoleService.WriteLine("\techo [text] - display text on next line");
                    m_consoleService.WriteLine("\ttime        - display current time");
                    m_consoleService.WriteLine("\tdate        - display current date");
                    m_consoleService.WriteLine("\tclear       - clear command line");
                    break;

                default:
                    m_consoleService.WriteLine("Try typing 'help' into the console and pressing enter.");
                    break;
            }
        }
    }
}
