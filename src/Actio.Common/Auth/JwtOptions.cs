namespace Actio.Common.Auth
{
    public class JwtOptions
    {
        public string SecurityKey { get; set; }
        public string Issuer { get; set; }
        public double ExpiryMinutes { get; set; }
    }
}