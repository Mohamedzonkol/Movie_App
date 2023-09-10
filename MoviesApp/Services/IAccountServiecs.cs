using MoviesApp.Data;
using MoviesApp.DTO;
using System.IdentityModel.Tokens.Jwt;

namespace MoviesApp.Services
{
    public interface IAccountServiecs
    {
        Task<AuthDTO> Login(LoginDTO userDto);
        Task<AuthDTO> Regester(RegesterDTO regester);
    }
}