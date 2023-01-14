namespace ptuswph_backend.Models.Settings
{
    public class JWTSettings
    {
        public string Issuer { get; set; } = "localhost";
        public string Audience { get; set; } = "localhost";
        public string Secret { get; set; } = "password12345678";
    }
}
