using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MoviesApp.Data;
using MoviesApp.DTO;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace MoviesApp.Services
{
    public class AccountServiecs : IAccountServiecs
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IConfiguration config;

        public AccountServiecs(UserManager<AppUser> _userManager, IConfiguration _config)
        {
            userManager = _userManager;
            config = _config;
        }
        public async Task<AuthDTO> Regester(RegesterDTO regester)
        {
            if (await userManager.FindByEmailAsync(regester.Email) is not null)
                return new AuthDTO { Message = "This Email Is Already Exist" };
            if (await userManager.FindByNameAsync(regester.Name) is not null)
                return new AuthDTO { Message = "This USer Name Is Already Exist" };
            AppUser user = new AppUser
            {
                UserName = regester.Name,
                Email = regester.Email,

            };
            IdentityResult result = await userManager.CreateAsync(user, regester.Password);
            if (result.Succeeded == false)
            {
                var errors = string.Empty;
                foreach (var item in result.Errors)
                    errors += $"{item.Description}";
                return new AuthDTO { Message = errors };
            }
            await userManager.AddToRoleAsync(user, "User");
            JwtSecurityToken userToken = await CreateJwtToken(user);
            return new AuthDTO
            {
                UserName = user.UserName,
                Email = user.Email,
                IsAuthuriza = true,
                Token = new JwtSecurityTokenHandler().WriteToken(userToken),
                ExpiredOn = userToken.ValidTo,
                UserRole = new List<string> { "User" }
            };
        }
        public async Task<AuthDTO> Login(LoginDTO userDto)
        {
            AuthDTO auth = new AuthDTO();
            AppUser user = await userManager.FindByEmailAsync(userDto.Emial);
            if (user == null)
            {
                auth.Message = "This Email Is Not Found";
                return auth;
            };
            bool result = await userManager.CheckPasswordAsync(user, userDto.Password);
            if (!result)
            {
                auth.Message = "This Email or Password Is False";
                return auth;
            }


            var Roles = await userManager.GetRolesAsync(user);
            JwtSecurityToken userToken = await CreateJwtToken(user);

            auth.UserName = user.UserName;
            auth.Email = user.Email;
            auth.IsAuthuriza = true;
            auth.Token = new JwtSecurityTokenHandler().WriteToken(userToken);
            auth.ExpiredOn = userToken.ValidTo;
            auth.UserRole = Roles.ToList();
            return auth;
        }

        private async Task<JwtSecurityToken> CreateJwtToken(AppUser user)
        {
            var userClaim = await userManager.GetClaimsAsync(user);
            var userRole = await userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            foreach (var role in userRole)
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
            //Claims Token
            var Claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email,user.Email),
            new Claim(ClaimTypes.NameIdentifier,user.Id),
            }.Union(userClaim).Union(roleClaims);
            //Key
            SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"]));
            SigningCredentials SignInCred = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: config["JWT:ValidIssuerValidIssuer"],
                audience: config["JWT:ValidAudiance"],
                claims: Claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: SignInCred);
            return token;
        }
    }
}
