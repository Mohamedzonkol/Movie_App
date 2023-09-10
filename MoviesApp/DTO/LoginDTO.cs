using System.ComponentModel.DataAnnotations;

namespace MoviesApp.DTO
{
    public class LoginDTO
    {
        [Required]
        public string Emial { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
