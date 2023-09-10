using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.DTO;
using MoviesApp.Model;
using MoviesApp.Services;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace MoviesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IUnitOfWork unit;

        public MoviesController(IUnitOfWork _unit)
        {
            unit = _unit;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MovieDTO newMovie)
        {
            if (ModelState.IsValid)
            {
                if (newMovie.Poster == null)
                    ModelState.AddModelError("", "Please Enter A Vaild Poster");

                bool isVaild = await unit.genreService.IsVaild(newMovie.GnereId);
                if (!isVaild)
                    return BadRequest($"Not Found Genre with this id : {newMovie.GnereId}");

                using var strem = new MemoryStream();
                await newMovie.Poster.CopyToAsync(strem);
                Movie movie = new Movie
                {
                    Title = newMovie.Title,
                    Rate = newMovie.Rate,
                    Year = newMovie.Year,
                    Description = newMovie.Description,
                    GenreId = newMovie.GnereId,
                    Poster = strem.ToArray()

                };

                await unit.moviesService.Add(movie);
                return Ok(newMovie);
            }
            return BadRequest("Please try Again");
        }
        [HttpGet]
        public async Task<IActionResult> getAllMovies()
        {
            IEnumerable<Movie> moview = await unit.moviesService.getAll();
            return Ok(moview);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Movie movie = await unit.moviesService.getById(id);
            if (movie == null)
                return NotFound("This Movie Is Not Found");
            await unit.moviesService.Delete(movie);
            return Ok(movie);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] MovieDTO movieDto)
        {
            Movie movie = await unit.moviesService.getById(id);
            if (movie == null)
                return NotFound("This Movie Is Not Found");
            if (movieDto.Poster != null)
            {
                using var strem = new MemoryStream();
                await movieDto.Poster.CopyToAsync(strem);
                movie.Poster = strem.ToArray();
            }
            movie.Title = movieDto.Title;
            movie.Year = movieDto.Year;
            movie.Description = movieDto.Description;
            movie.Rate = movieDto.Rate;
            movie.GenreId = movieDto.GnereId;
            await unit.moviesService.Update(movie);
            return Ok(movie);
        }

        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> Search(string movieName)
        {
           Movie movie = await unit.moviesService.Search(movieName);
            if (movie == null || movieName == "") return NotFound($"This {movieName}is Not Found");
            return Ok(movie);
        }
    }
}
