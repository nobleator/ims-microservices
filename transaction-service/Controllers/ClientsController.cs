using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using transaction_service.Domain;
using transaction_service.Domain.Entities;
using DTO = transaction_service.Domain.DataTransferObjects;

namespace transaction_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        // GET api/clients
        [HttpGet]
        public ActionResult<List<DTO.Client>> Get()
        {
            List<DTO.Client> clientList;

            using (var db = new TransactionServiceDbContext())
            {
                clientList = db.Clients
                    .Where(c => !c.Deleted)
                    .Select(c => c.toDto())
                    .ToList();
            }

            return clientList;
        }

        // GET api/clients/5
        [HttpGet("{id}")]
        public ActionResult<DTO.Client> Get(int id)
        {
            DTO.Client client;

            using (var db = new TransactionServiceDbContext())
            {
                client = db.Clients
                    .Where(c => !c.Deleted &&
                                c.ClientId == id)
                    .Select(c => c.toDto())
                    .FirstOrDefault();
            }

            if (client == null)
            {
                client = new DTO.Client
                {
                    ClientId = -1,
                    Name = "<name>",
                    Description = "<description>"
                };
            }

            return client;
        }

        // POST api/clients
        [HttpPost]
        public void Post([FromBody] DTO.Client client)
        {
            Console.WriteLine($"DEBUG: Entering {nameof(Post)}");
            Console.WriteLine($"DEBUG: Request body contained: {client}");
            // TODO: updatedBy
            var updatedBy = "SYSTEM";
            using (var db = new TransactionServiceDbContext())
            {
                var dbClient = db.Clients.FindAsync(client.ClientId).Result;
                if (dbClient == null)
                {
                    // PK is serial, so should be automatically generated
                    dbClient = new Domain.Entities.Client
                    {
                        CreatedBy = updatedBy,
                        CreatedOn = DateTime.UtcNow
                    };
                    db.Add(dbClient);
                }
                dbClient.Name = client.Name;
                dbClient.Description = client.Description;
                dbClient.UpdatedBy = updatedBy;
                dbClient.UpdatedOn = DateTime.UtcNow;
                db.SaveChangesAsync();
            }
        }

        // PUT api/values/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody] string value)
        // {
        // }

        // DELETE api/clients/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using (var db = new TransactionServiceDbContext())
            {
                var dbClient = db.Clients.FindAsync(id).Result;
                if (dbClient != null)
                {
                    dbClient.Deleted = true;
                }
                db.SaveChangesAsync();
            }
        }
    }
}
