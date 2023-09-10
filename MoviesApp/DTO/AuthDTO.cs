namespace MoviesApp.DTO
{
    public class AuthDTO
    {
        public string Message { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsAuthuriza { get; set; }
        public string Token { get; set; }
        public List<string> UserRole { get; set; }
        public DateTime ExpiredOn { get; set; }

    }
}
