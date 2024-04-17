using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace AutoTeszt.Models.Commands.Models
{
    internal class MonitorBrightnessCommand : ATestCommand
    {
        public MonitorBrightnessCommand() : base()
        {
            m_executionId = "brightness";
        }
        public override void Execute(object prop)
        {
            int brightness = 0;
            if (!int.TryParse(((IEnumerable<string>)prop).ElementAt(0), out brightness))
                brightness = 100;
            Console.WriteLine($"Set monitor brightness -> {brightness}");
            Set(brightness);
        }
        public static int Get()
        {
            using (var mclass = new ManagementClass("WmiMonitorBrightness"))
            {
                var Scope = new ManagementScope(@"\\.\root\wmi");
                using (var instances = mclass.GetInstances()) {
                    foreach (ManagementObject instance in instances)
                    {
                        return (byte)instance.GetPropertyValue("CurrentBrightness");
                    }
                }
            }
            return 0;
        }

        public static void Set(int brightness)
        {
            using (var mclass = new ManagementClass("WmiMonitorBrightnessMethods"))
            {
                var Scope = new ManagementScope(@"\\.\root\wmi");
                using (var instances = mclass.GetInstances())
                {
                    var args = new object[] { 1, brightness };
                    foreach (ManagementObject instance in instances)
                    {
                        instance.InvokeMethod("WmiSetBrightness", args);
                    }
                }
            }
        }
    }
}
