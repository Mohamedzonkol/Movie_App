using MoviesApp.Model;
using System.ComponentModel.DataAnnotations;

namespace MoviesApp.DTO
{
    public class RegesterDTO
    {
        [Required,Display(Name="User Name")]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required,Compare("Password")]
        public string ConfirmPassword { get; set; }



    }
}
