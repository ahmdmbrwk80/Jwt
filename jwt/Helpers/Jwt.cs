namespace jwt.Helpers
{
    public class Jwt
    {
        public string key { get; set; } = string.Empty;
        public string auddience { get; set; } = string.Empty;
        public double duretionindayes { get; set; }
        public string issuer { get; set; } = string.Empty;

    }
}
