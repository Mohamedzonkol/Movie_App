using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Model
{
    public class Movie
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Title { get; set; }
        public double Rate { get; set; }
        public int Year { get; set; }
        public byte[] Poster { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        
        public  int GenreId {get; set; }
        
        public Genre Genre { get; set; }    

    }
}
