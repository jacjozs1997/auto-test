using AutoTeszt.Models.Console.Internal;
using AutoTeszt.Models.Utilities;
using AutoTeszt.Views;
using System;
using System.Collections.Generic;

namespace AutoTeszt.Models.Console.Services
{
    public class ConsoleService : Singleton<ConsoleService>
    {
        private string m_consoleName;

        public ConsoleService() : this("DefaultAutoTestConsole")
        {

        }

        public ConsoleService(string consoleName)
        {
            if (consoleName == null)
                throw new ArgumentNullException("consoleName");
            if (string.IsNullOrWhiteSpace(consoleName))
                throw new ArgumentException("Argument 'consoleName' should be a valid console name.");

            m_consoleName = consoleName;
        }

        public void Clear()
        {
            foreach (var console in Consoles)
                console.Clear();
        }

        public void WriteLine(string text)
        {
            foreach (var console in Consoles)
                console.WriteLine(text);
        }

        public void Write(string text)
        {
            foreach (var console in Consoles)
                console.Write(text);
        }

        private IEnumerable<ConsoleControl> Consoles
        {
            get
            {
                return ConsoleRegister.Retrieve(m_consoleName);
            }
        }
    }
}
