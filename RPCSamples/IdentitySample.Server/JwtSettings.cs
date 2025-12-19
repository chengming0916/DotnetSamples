namespace IdentitySample.Server
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public string Issuer { get; internal set; }
        public string Audience { get; internal set; }
    }
}
