using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.DTO;
using MoviesApp.Services;

namespace MoviesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork unit;

        public AccountController(IUnitOfWork _unit)
        {
            unit = _unit;
        }
        [HttpPost("Regester")]
        public async Task<IActionResult> Regester([FromForm] RegesterDTO regester)
        {
            if (ModelState.IsValid)
            {
                var result = await unit.accountServiecs.Regester(regester);
                if (!result.IsAuthuriza)
                    return Unauthorized();
                return Ok(result);

            }
            return BadRequest("Please Try again");
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm] LoginDTO login)
        {
            if (ModelState.IsValid)
            {
                var result = await unit.accountServiecs.Login(login);
                if (!result.IsAuthuriza)
                    return Unauthorized();
                return Ok(result);

            }
            return BadRequest("Please Try again");
        }

    }
}
