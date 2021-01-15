using AuditApp.Controllers.Dto;
using AuditApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditApp.Controllers
{
    [ApiController]
    public class CRUDController : ControllerBase
    {
        public readonly ApplicationDbContext dbContext;
        public CRUDController(ApplicationDbContext DbContext)
        {
            dbContext = DbContext;
        }

        [HttpGet]
        [Route("api/GetAllData")]
        public async Task<List<PeopleDto>> GetAllData()
        {
            return await dbContext.People.Select(x => new PeopleDto
            {
                Name = x.Name,
                Address = x.Address,
            }).ToListAsync();
        }

        [Route("api/CreateData")]
        [HttpPost]
        public async Task<bool> CreateData([FromBody] PeopleDto createPlayer)
        {
            var create = new Person()
            {
                Name = createPlayer.Name,
                Address = createPlayer.Address
            };
            await dbContext.People.AddAsync(create);
            await dbContext.SaveChangesAsync();
            return true;
        }


        [Route("api/UpdateData")]
        [HttpPut]
        public async Task<bool> UpdateData([FromBody] PeopleDto update)
        {
            var data = await dbContext.People.Where(x => x.Name == update.Name).FirstOrDefaultAsync();
            data.Name = update.Name;                    
            data.Address = update.Address;
            dbContext.People.Update(data);
            await dbContext.SaveChangesAsync();
            return true;
        }


        [Route("api/DeleteData/{Id}")]
        [HttpDelete]
        public async Task<bool> DeleteData(int Id)
        {
            var data = await dbContext.People.Where(x => x.Id == Id).FirstOrDefaultAsync();
            dbContext.People.Remove(data);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
