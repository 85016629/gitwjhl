using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fx.api.Authentication.Models;
using fx.Domain.Customer;
using Microsoft.AspNetCore.Mvc;

namespace fx.api.Authentication.Controllers
{
    public class AuthController : Controller
    {
        private IAuthenticationService _service;
        public IActionResult Index()
        {
            return View();
        }

        public AuthController(IAuthenticationService service)
        {
            _service = service;
        }

        [HttpPut]
        [Route("/auth/login")]
        public IActionResult Login([FromBody] LoginViewModel loginViewModel)
        {

            if (_service.Login(loginViewModel.UserLoginId, loginViewModel.UserLoginId))
                return Ok("");
            return Ok();
        }
    }
}