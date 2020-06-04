namespace Aerith.Api.Settings
{
    public class JwtSettings
    {
        public string Audience { get; set; }
        public string HmacSecretKey { get; set; }
        public string Issuer { get; set; }
        public int TokenExpiryMinutes { get; set; }
        public string TokenName { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
        public bool ValidateLifetime { get; set; }
    }
}