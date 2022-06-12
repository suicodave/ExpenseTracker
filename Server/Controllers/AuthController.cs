using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using Server.Users;

using Shared.Auth;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;

        public AuthController(SignInManager<User> signInManager, IConfiguration configuration, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _configuration = configuration;
            _userManager = userManager;
        }


        [HttpPost]
        public async Task<ActionResult> SignIn([FromBody] SignInRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return NotFound();
            }

            var result = await this._signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            var userRoles = await _userManager.GetRolesAsync(user);

            if (!result.Succeeded)
            {
                return NotFound();
            }

            // Claims Encoding
            List<Claim> claims = new();
            claims.Add(new Claim(ClaimTypes.Name, request.Email));

            claims.AddRange(userRoles.Select(x => new Claim(ClaimTypes.Role, x)));



            // Encryption
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(int.Parse(_configuration["JwtExpiryInDays"]));


            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtAudience"],
                claims,
                expires: expiry,
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();

            string tokenString = tokenHandler.WriteToken(token);


            return Ok(tokenString);
        }
    }
}