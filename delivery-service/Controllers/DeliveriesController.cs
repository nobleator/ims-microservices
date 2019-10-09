using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using delivery_service.Domain.DataTransferObjects;
using Newtonsoft.Json;

namespace delivery_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveriesController : ControllerBase
    {
        // GET api/deliveries
        // [HttpGet]
        // public ActionResult<List<DTO.Transaction>> Get()
        // {
            
        // }

        // GET api/deliveries/5
        // [HttpGet("{id}")]
        // public ActionResult<DTO.Transaction> Get(int id)
        // {
            
        // }

        // POST api/deliveries
        [HttpPost]
        public void Post()
        {
            Console.WriteLine($"DEBUG: Entering {nameof(Post)}");
            Console.WriteLine($"DEBUG: No body expected, queueing new delivery schedule");

            // Get all line items that are flagged "Ready to ship" 
            // Ensure destination and profitability are available or calculatible
            var candidates = new List<LineItem>
            {
                new LineItem
                {
                    LineItemId = -1,
                    TransactionId = -1,
                    ProductId = -1,
                    Quantity = 10,
                    Price = 100
                }
            };
            var candidatesStr = JsonConvert.SerializeObject(candidates);
            var outgoingBody = Encoding.UTF8.GetBytes(candidatesStr);

            // TODO: broker is the connected RabbitMQ Docker container. Update to pull from config/env settings
            var factory = new ConnectionFactory() { HostName = "broker" };
            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "optimization_jobs",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                channel.BasicPublish(exchange: "",
                                     routingKey: "optimization_jobs",
                                     basicProperties: null,
                                     body: outgoingBody);
                Console.WriteLine(" [x] Sent {0}", candidates);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

        // PUT api/values/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody] string value)
        // {
        // }

        // DELETE api/deliveries/5
        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
            
        // }
    }
}
