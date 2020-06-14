namespace Aerith.Api.Settings
{
    public class JwtSettings
    {
        public string Audience { get; set; }
        public double ClockSkewMinutes { get; set; }
        public string HmacSecretKey { get; set; }
        public string Issuer { get; set; }
        public bool RequireSignedTokens { get; set; }
        public bool RequireExpirationTime { get; set; }
        public int TokenExpiryMinutes { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
        public bool ValidateLifetime { get; set; }
    }
}