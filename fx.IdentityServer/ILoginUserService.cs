namespace fx.IdentityService
{
    public interface ILoginUserService
    {
        bool Authenticate(string userName, string password, out LoginUser loginUser);
    }
}