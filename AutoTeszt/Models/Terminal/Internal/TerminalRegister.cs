using AutoTeszt.Views;
using System;
using System.Collections.Generic;

namespace AutoTeszt.Models.Terminal.Internal
{
    static class TerminalRegister
    {
        static readonly Dictionary<string, List<TerminalControl>> m_register;
        static TerminalRegister()
        {
            m_register = new Dictionary<string, List<TerminalControl>>();
        }

        public static void Register(TerminalControl control)
        {
            List<TerminalControl> list;
            if (m_register.ContainsKey(control.Name))
            {
                list = m_register[control.Name];
            }
            else
            {
                list = new List<TerminalControl>();
                m_register[control.Name] = list;
            }
            list.Add(control);
        }

        public static IEnumerable<TerminalControl> Retrieve(string consoleName)
        {
            if (!m_register.ContainsKey(consoleName))
                throw new InvalidOperationException("Console should be loaded first.");

            return m_register[consoleName];
        }

        public static void Unregister(TerminalControl control)
        {
            if (m_register.ContainsKey(control.Name))
            {
                var list = m_register[control.Name];
                list.Remove(control);
            }
        }
    }
}
