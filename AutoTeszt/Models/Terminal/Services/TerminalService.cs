using AutoTeszt.Models.Terminal.Internal;
using AutoTeszt.Models.Utilities;
using AutoTeszt.Views;
using System;
using System.Collections.Generic;

namespace AutoTeszt.Models.Terminal.Services
{
    public class TerminalService : Singleton<TerminalService>
    {
        private readonly string m_terminalName;

        public TerminalService() : this("DefaultAutoTestTerminal")
        {

        }

        public TerminalService(string terminalName)
        {
            if (terminalName == null)
                throw new ArgumentNullException("terminalName");
            if (string.IsNullOrWhiteSpace(terminalName))
                throw new ArgumentException("Argument 'terminalName' should be a valid terminal name.");

            m_terminalName = terminalName;
        }

        public void Clear()
        {
            foreach (var terminal in Terminals)
                terminal.Clear();
        }

        public void WriteLine(string text)
        {
            foreach (var terminal in Terminals)
                terminal.WriteLine(text);
        }

        public void Write(string text)
        {
            foreach (var terminal in Terminals)
                terminal.Write(text);
        }

        private IEnumerable<TerminalControl> Terminals
        {
            get
            {
                return TerminalRegister.Retrieve(m_terminalName);
            }
        }
    }
}
