using System.ComponentModel.DataAnnotations;

namespace MoviesApp.DTO
{
    public class MovieDTO
    {
        [MaxLength(255)]
        public string Title { get; set; }
        public double Rate { get; set; }
        public int Year { get; set; }
        [MaxLength(1000)]

        public string Description { get; set; }
        [Required]
        public IFormFile Poster { get; set; }
        public int GnereId { get; set; }
  
        }
}
