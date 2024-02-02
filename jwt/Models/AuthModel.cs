namespace jwt.Models
{
    public class AuthModel
    {

        public string Message { get; set; }
        public bool IsAuthentecated { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string token { get; set; } = string.Empty;
        public DateTime Expireson { get; set; }

    }
}
