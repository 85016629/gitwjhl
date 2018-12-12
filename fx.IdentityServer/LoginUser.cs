using System.Security.Claims;

namespace fx.IdentityService
{
    public class LoginUser
    {
        public object Id { get; internal set; }
        public string RealName { get; internal set; }
        public string Email { get; internal set; }
    }
}