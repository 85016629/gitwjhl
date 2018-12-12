namespace fx.ApiGateway
{
    internal class IdentityServerAuthenticationOptions
    {
        public string Authority { get; internal set; }
        public string ApiName { get; internal set; }
        public bool RequireHttpsMetadata { get; internal set; }
        public object SupportedTokens { get; internal set; }
        public string ApiSecret { get; internal set; }
    }

    public enum SupportedTokens
    {
        Both = 0
    }
}