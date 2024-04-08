using System;

namespace AutoTeszt.Models.Console
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
