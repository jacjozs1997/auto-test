using AutoTeszt.Views;
using System;
using System.Windows;

namespace AutoTeszt.Models.Terminal.Helpers
{
    static class TerminalHelpers
    {
        public static void PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e, Action<TerminalControl, DependencyPropertyChangedEventArgs> action)
        {
            var control = (TerminalControl)d;
            if (control != null)
            {
                action(control, e);
            }
        }
    }
}
