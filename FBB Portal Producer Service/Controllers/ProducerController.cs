using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FBB_Portal_Service.Controllers
{

    [ApiController]
    [Route("api/")]
    public class ProducerController : ControllerBase
    {
        private ProducerConfig _config;
        public ProducerController(ProducerConfig config)
        {
            this._config = config;
        }


        [HttpGet("send")]
        public async Task<ActionResult> GetAsync()
        {
            //string serializedEmployee = JsonConvert.SerializeObject(employee);
            string serializedEmployee = "{\"A\":\"B\"}";

            string topic = "HOME_CHECKIN_CREATED";
            using (var producer = new ProducerBuilder<Null, string>(_config).Build())
            {
                await producer.ProduceAsync(topic, new Message<Null, string> { Value = serializedEmployee });
                producer.Flush(TimeSpan.FromSeconds(10));
                return Ok("Okkk");
            }
        }

        public class Employee
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
