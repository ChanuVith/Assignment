using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Assignment.Application.Helpers;
using Assignment.Domain.DTO_s;
using Assignment.Domain.DTO_s.Auth;
using Assignment.Domain.Entities;
using Assignment.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Assignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _authContext;
        private IConfiguration _config;
        public AuthController(AppDbContext context, IConfiguration config)
        {
            _authContext = context;
            _config = config;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Authenticate([FromBody] UserLoginDto userObj)
        {
            if (userObj == null)
                return BadRequest();

            var user = await _authContext.User
    .FirstOrDefaultAsync(x => x.Email == userObj.Email && x.Password == userObj.Password);


            if (user == null)
                return NotFound(new { Message = "User not found!" });

            // Check if the user's token is blacklisted
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var isBlacklisted = await _authContext.BlacklistedTokens
                .AnyAsync(t => t.Token == token);

            if (isBlacklisted)
            {
                // Token is blacklisted; return an unauthorized response
                return Unauthorized(new { Message = "Token is blacklisted. User has been logged out." });
            }

            user.Token = CreateJwt(user);
            var newAccessToken = user.Token;
            var newRefreshToken = CreateRefreshToken();
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(5);
            await _authContext.SaveChangesAsync();

            return Ok(new TokenApiDto()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                // Get the token from the request headers
                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                // Check if the token is already blacklisted
                var isBlacklisted = await _authContext.BlacklistedTokens
                    .AnyAsync(t => t.Token == token);

                if (!isBlacklisted)
                {
                    // Add the token to the blacklist
                    var blacklistedToken = new BlacklistedToken { Token = token };
                    _authContext.BlacklistedTokens.Add(blacklistedToken);
                    await _authContext.SaveChangesAsync();
                }

                return Ok("Logged out successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while logging out.");
            }
        }

       
        private string CreateJwt(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Name, user.Username)
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddMinutes(15),
                SigningCredentials = credentials
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }

        private string CreateRefreshToken()
        {
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var refreshToken = Convert.ToBase64String(tokenBytes);

            var tokenInUser = _authContext.User
                .Any(a => a.RefreshToken == refreshToken);
            if (tokenInUser)
            {
                return CreateRefreshToken();
            }
            return refreshToken;
        }

        //private ClaimsPrincipal GetPrincipleFromExpiredToken(string token)
        //{
        //    var key = Encoding.ASCII.GetBytes("veryverysceret.....");
        //    var tokenValidationParameters = new TokenValidationParameters
        //    {
        //        ValidateAudience = false,
        //        ValidateIssuer = false,
        //        ValidateIssuerSigningKey = true,
        //        IssuerSigningKey = new SymmetricSecurityKey(key),
        //        ValidateLifetime = false
        //    };
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    SecurityToken securityToken;
        //    var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        //    var jwtSecurityToken = securityToken as JwtSecurityToken;
        //    if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        //        throw new SecurityTokenException("This is Invalid Token");
        //    return principal;
        //}

        //[HttpPost("refresh")]
        //public async Task<IActionResult> Refresh([FromBody] TokenApiDto tokenApiDto)
        //{
        //    if (tokenApiDto is null)
        //        return BadRequest("Invalid Client Request");
        //    string accessToken = tokenApiDto.AccessToken;
        //    string refreshToken = tokenApiDto.RefreshToken;
        //    var principal = GetPrincipleFromExpiredToken(accessToken);
        //    var username = principal.Identity.Name;
        //    var user = await _authContext.User.FirstOrDefaultAsync(u => u.Username == username);
        //    if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
        //        return BadRequest("Invalid Request");
        //    var newAccessToken = CreateJwt(user);
        //    var newRefreshToken = CreateRefreshToken();
        //    user.RefreshToken = newRefreshToken;
        //    await _authContext.SaveChangesAsync();
        //    return Ok(new TokenApiDto()
        //    {
        //        AccessToken = newAccessToken,
        //        RefreshToken = newRefreshToken,
        //    });
        //}



    }
}
