using System;

namespace AutoTeszt.Models.Terminal
{
    public class ExecuteEventArgs : EventArgs
    {
        internal ExecuteEventArgs(string command)
        {
            Command = command;
        }

        public string Command { get; private set; }
    }
}
