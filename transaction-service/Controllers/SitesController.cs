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
    public class SitesController : ControllerBase
    {
        // GET api/sites
        [HttpGet]
        public ActionResult<List<DTO.Site>> Get()
        {
            List<DTO.Site> siteList;

            using (var db = new TransactionServiceDbContext())
            {
                siteList = db.Sites
                    .Where(s => !s.Deleted)
                    .OrderByDescending(s => s.Address)
                    .Select(s => s.toDto())
                    .ToList();
            }

            return siteList;
        }

        // GET api/sites/5
        [HttpGet("{id}")]
        public ActionResult<DTO.Site> Get(int id)
        {
            DTO.Site site;

            using (var db = new TransactionServiceDbContext())
            {
                site = db.Sites
                    .Where(s => !s.Deleted &&
                                s.SiteId == id)
                    .Select(s => s.toDto())
                    .FirstOrDefault();
            }

            return site;
        }

        // POST api/sites
        [HttpPost]
        public void Post([FromBody] DTO.Site site)
        {
            // Sites should not be created directly, only in relation to a transaction?
            // TODO: updatedBy
            var updatedBy = "SYSTEM";
            using (var db = new TransactionServiceDbContext())
            {
                var dbSite = db.Sites.FindAsync(site.SiteId).Result;
                if (dbSite != null)
                {
                    dbSite.Address = site.Address;
                    dbSite.Description = site.Description;
                    dbSite.Latitude = site.Latitude;
                    dbSite.Longitude = site.Longitude;
                    dbSite.UpdatedOn = DateTime.UtcNow;
                    dbSite.UpdatedBy = updatedBy;
                    db.SaveChangesAsync();
                }
            }
        }

        // PUT api/values/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody] string value)
        // {
        // }

        // DELETE api/sites/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using (var db = new TransactionServiceDbContext())
            {
                var dbSite = db.Sites.FindAsync(id).Result;
                if (dbSite != null)
                {
                    dbSite.Deleted = true;
                }
                db.SaveChangesAsync();
            }
        }
    }
}
