using AutoTeszt.Views;
using System;
using System.Collections.Generic;

namespace AutoTeszt.Models.Console.Internal
{
    static class ConsoleRegister
    {
        static readonly Dictionary<string, List<ConsoleControl>> m_register;
        static ConsoleRegister()
        {
            m_register = new Dictionary<string, List<ConsoleControl>>();
        }

        public static void Register(ConsoleControl control)
        {
            List<ConsoleControl> list;
            if (m_register.ContainsKey(control.Name))
            {
                list = m_register[control.Name];
            }
            else
            {
                list = new List<ConsoleControl>();
                m_register[control.Name] = list;
            }
            list.Add(control);
        }

        public static IEnumerable<ConsoleControl> Retrieve(string consoleName)
        {
            if (!m_register.ContainsKey(consoleName))
                throw new InvalidOperationException("Console should be loaded first.");

            return m_register[consoleName];
        }

        public static void Unregister(ConsoleControl control)
        {
            if (m_register.ContainsKey(control.Name))
            {
                var list = m_register[control.Name];
                list.Remove(control);
            }
        }
    }
}
