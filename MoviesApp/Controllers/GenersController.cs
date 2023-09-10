using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.DTO;
using MoviesApp.Model;
using MoviesApp.Services;

namespace MoviesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenersController : ControllerBase
    {
        private readonly IUnitOfWork unit;
        public GenersController(IUnitOfWork _unit)
        {
            unit = _unit;
        }
        [HttpGet]
        public async Task<IActionResult> getAll()
        {
          IEnumerable<Genre> Geners=await unit.genreService.getAll();
            return Ok(Geners);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]GenersDto newGenere)
        {
            if (ModelState.IsValid)
            {
               Genre genre = new Genre { Name=newGenere.Name};
                await unit.genreService.Add(genre);
                return Ok(genre);
            
            }
            return BadRequest("Please try again");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Updata(int id, [FromForm] GenersDto Genere)
        {
            Genre genre = await unit.genreService.getById(id);
            if (genre == null)
                return NotFound("This Genre Is Not Found");
            genre.Name= Genere.Name;
            await unit.genreService.Update(genre);
            return Ok(genre);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var genre = await unit.genreService.getById(id);
            if (genre == null)
                return NotFound($"Not found Gnere with id {id} ");

            await unit.genreService.Delete(genre);
            return Ok(genre);

        }
    }
}
