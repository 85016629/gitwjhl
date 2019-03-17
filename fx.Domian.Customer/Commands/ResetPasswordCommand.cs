using fx.Domain.core;

namespace fx.Domain.CustomerContext.Commands
{
    public class ResetPasswordCommand : BaseCommand
    {
        ResetPasswordCommand(string oldPasswd, string newPasswd, string loginId)
        {
            OldPasswd = oldPasswd;
            NewPasswd = newPasswd;
            LoginId = loginId;
        }

        public string OldPasswd { get; set; }
        public string NewPasswd { get; set; }
        public string LoginId { get; set; }

    }
}