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
    using fx.IdentityService.common;
    using System.Security.Principal;
    using System.Linq;
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;
    using IdentityServer4.Services;
    using Microsoft.AspNetCore.Authentication;
    using IAuthenticationService = Application.Customer.IAuthenticationService;
    using IdentityServer4.Events;


    public class AuthenticationController : ControllerBase
    {
        public IAuthenticationService _authenticationService;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IEventService _events;
        public AuthenticationController(IAuthenticationService authenticationService, IIdentityServerInteractionService interaction, IEventService eventService)
        {
            _authenticationService = authenticationService;
            _interaction = interaction;
            _events = eventService;
        }
        [HttpGet]
        public IActionResult Authenticate()
        {
            return Ok("test");
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
                    new Claim(JwtClaimTypes.PhoneNumber, user.MobilePhone?.ToString())
                }),
                Expires = expiresAt,
                SigningCredentials = TokenAuthOption.SigningCredentials,
                //SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)                ,
                Issuer = TokenAuthOption.Issuer,
                Audience = TokenAuthOption.Audience
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

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel login)
        {
            var user = _authenticationService.GetUserByLoginIdAndPassword(login.UserLoginId, login.Password);
            if (user == null)
                return Unauthorized();

            await _events.RaiseAsync(new UserLoginSuccessEvent(user.Username, user.UUId.ToString(), user.Username));

            var context = await _interaction.GetAuthorizationContextAsync(login.ReturnUrl);

            AuthenticationProperties props = null;

            var claims = new Claim[]
                {
                    new Claim(JwtClaimTypes.Audience,"api"),
                    new Claim(JwtClaimTypes.Issuer,"http://localhost:5200"),
                    new Claim(JwtClaimTypes.Id, user.UUId.ToString()),
                    new Claim(JwtClaimTypes.Name, user.Username),
                    //new Claim(JwtClaimTypes.Email, user.Email),
                    new Claim(JwtClaimTypes.PhoneNumber, user.MobilePhone)
                };

            await HttpContext.SignInAsync(user.UUId.ToString(), claims);

            if (context != null)
                Redirect(login.ReturnUrl);

            // request for a local page
            if (Url.IsLocalUrl(login.ReturnUrl))
            {
                return Redirect(login.ReturnUrl);
            }

            return Ok(HttpContext.Request.Headers["Token"]);
        }
    }
}
