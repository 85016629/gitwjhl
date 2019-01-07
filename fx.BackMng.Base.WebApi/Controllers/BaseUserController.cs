namespace fx.BackMng.Base.WebApi.Controllers
{
    using System.Collections.Generic;
    using fx.Domain.core;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    public class BaseUserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public BaseUserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("BackManagementSystem/BaseUsers")]
        public IActionResult GetAllUsers(int pageIndex, int pageSize)
        {
            var users = _userRepository.SearchUsersPages(pageIndex, pageSize);
            if (users == null || users.Count == 0)
                return Ok("");
            return Ok(users);
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("BackManagementSystem/BaseUsers/{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
