using System.Diagnostics;
using System.Globalization;

namespace AutoTeszt.Models.Commands.Models
{
    internal class AdminOnCommand : ATestCommand
    {
        public AdminOnCommand() : base()
        {
            m_executionId = "adminon";
        }
        public override void Execute(object prop)
        {
            Process.Start("net", $"user {AdminName()} /active:yes");
        }
        private string AdminName()
        {
            CultureInfo ci = CultureInfo.InstalledUICulture;
            switch(ci.TwoLetterISOLanguageName)
            {
                case "hu":
                    return "Rendszergazda";
                case "fr":
                    return "Administrateur";
                case "pt":
                    return "Administrador";
                case "sv":
                    return "Administratör";
                case "fi":
                    return "Järjestelmänvalvoja";
                case "bg":
                    return "Администратор";
            }
            return "Administrator";
        }
    }
}
