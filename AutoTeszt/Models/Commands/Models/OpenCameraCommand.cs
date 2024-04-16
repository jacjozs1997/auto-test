using System;
using System.Diagnostics;

namespace AutoTeszt.Models.Commands.Models
{
    internal class OpenCameraCommand : ATestCommand
    {
        public OpenCameraCommand() : base()
        {
            m_executionId = "camera";
        }
        public override void Execute(object prop)
        {
            Console.WriteLine("Start camera");
            Process.Start("microsoft.windows.camera:");
        }
    }
}
