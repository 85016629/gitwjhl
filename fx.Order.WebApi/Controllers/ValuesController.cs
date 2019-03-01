using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.CAP;
using fx.Infra.Data.SqlServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fx.Order.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("OrderService/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ICapPublisher _publisher;

        public ValuesController(ICapPublisher publisher)
        {
            _publisher = publisher;
        }

        // GET api/values
        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    return new string[] { "value1 from OrderService", "value2 from OrderService" };
        //}

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

        /// <summary>
        /// 演示通过RabbitMQ发送消息。
        /// 测试是OK的。
        /// </summary>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/checkAccountWithTrans")]        
        public async Task<IActionResult> PublishMessageWithTransaction([FromServices]CapDbContext dbContext)
        {
            using (var trans = dbContext.Database.BeginTransaction())
            {
                //指定发送的消息标题（供订阅）和内容
                await _publisher.PublishAsync("xxx.services.account.check",
                    new Person { Name = "Foo", Age = 11 });
                // 你的业务代码。
                trans.Commit();
            }
            return Ok();
        }

        //[NoAction]
        /// <summary>
        /// 演示通过Cap接收消息，演示是OK的。
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpGet]
        [CapSubscribe("xxx.services.account.check")]        
        public async Task<IActionResult> CheckReceivedMessage(Person person)
        {
            Console.WriteLine(person.Name);
            Console.WriteLine(person.Age);
            return Ok(); 
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}