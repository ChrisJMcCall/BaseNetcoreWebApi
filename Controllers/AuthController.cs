using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System.Linq;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using BaseWebApi.Models;

namespace BaseWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserContext _userContext;
        private readonly IConfiguration _configuration;
        public AuthController(UserContext userContext, IConfiguration configuration) {
            _userContext = userContext;
            _configuration = configuration;
        }

        [HttpPost("token")]
        public ActionResult<string> Create([FromForm] string userLogin, [FromForm] string password)
        {
            var user = _userContext.User.Single(b => b.Email.Equals(userLogin));

            var passwordHasher = new PasswordHasher<Models.User>();
            var verificationResult = passwordHasher.VerifyHashedPassword(user, user.Hash, password);
            
            if (verificationResult == PasswordVerificationResult.Success) {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("jwtKey")));
                var claims = new Claim[] {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Exp, $"{new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds()}"),
                    new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}")
                };

                var token = new JwtSecurityToken(new JwtHeader(new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)), new JwtPayload(claims));
                string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                return jwtToken;
            }
            return verificationResult.ToString();
        }
    }
}