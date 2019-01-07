namespace fx.IdentityService.Controllers
{
    using ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using fx.Application.Customer;
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;
    using System;
    using System.Security.Claims;
    using IdentityModel;
    using Microsoft.IdentityModel.Tokens;

    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        public IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody]LoginViewModel loginViewModel)
        {
            var user = _authenticationService.GetUserByLoginIdAndPassword(loginViewModel.UserLoginId, loginViewModel.Password);
            if (user == null)
                return Unauthorized();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Consts.Secret);
            var authTime = DateTime.UtcNow;
            var expiresAt = authTime.AddDays(7);//保留时间7天
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtClaimTypes.Audience,"api"),
                    new Claim(JwtClaimTypes.Issuer,"http://localhost:5200"),
                    new Claim(JwtClaimTypes.Id, user.UUId.ToString()),
                    new Claim(JwtClaimTypes.Name, user.Username),
                    //new Claim(JwtClaimTypes.Email, user.Email),
                    new Claim(JwtClaimTypes.PhoneNumber, user.MobilePhone)
                    //new Claim(JwtClaimTypes.Role,"admin")
                }),
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return Ok(new
            {
                access_token = tokenString,
                token_type = "Bearer",
                profile = new
                {
                    sid = user.UUId.ToString(),
                    name = user.Username,
                    auth_time = new DateTimeOffset(authTime).ToUnixTimeSeconds(),
                    expires_at = new DateTimeOffset(expiresAt).ToUnixTimeSeconds()
                }
            });
        }
    }
}
