namespace fx.ApiGateway
{
    public class ServiceRegisterOptions
    {
        public bool IsActive { get; set; }
        public string ServiceName { get; set; }
        public string ServiceHost { get; set; }
        public string ServicePort { get; set; }
        public RegisterOptions Register { get; set; }


        public class RegisterOptions
        {
            public string HttpEndpoint { get; set; }
        }
    }
}