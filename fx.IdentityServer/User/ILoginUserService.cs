namespace fx.IdentityService
{
    public interface ITestLoginUserService
    {
        bool Authenticate(string userName, string password, out LoginUser loginUser);
    }
}