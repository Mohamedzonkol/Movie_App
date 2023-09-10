using System.ComponentModel.DataAnnotations;

namespace MoviesApp.DTO
{
    public class GenersDto
    {
        [Required,MaxLength(100)]
        public string Name { get; set; }
    }
}
