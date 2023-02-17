using AareonTechnicalTest.Controllers.Utils;
using AareonTechnicalTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AareonTechnicalTest.Controllers
{

    public class TicketController : BaseController
    {
        public TicketController(ApplicationContext context) : base(context)
        {

        }
        // GET: api/<TicketController>
        [HttpGet]
        public async Task<List<Ticket>> Get()
        {
            List<Ticket> list = new List<Ticket>();
            list = await this.Context.Tickets.ToListAsync();
            return list;
        }

        // GET api/<TicketController>/5
        [HttpGet("{id}")]
        public async Task<Ticket> Get(int id)
        {
            return await this.Context.FindAsync<Ticket>(id);
        }

        // POST api/<TicketController>
        [HttpPost]
        public async Task<int> Post(Ticket ticket)
        {
            this.Context.Tickets.Add(ticket);
            var result = await this.Context.SaveChangesAsync();
            return result;

        }

        // PUT api/<TicketController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TicketController>/5
        [HttpDelete("{id}")]
        public async Task<int> Delete(int id)
        {
            var ticket = await this.Context.FindAsync<Ticket>(id);
            this.Context.Remove<Ticket>(ticket);
            return await this.Context.SaveChangesAsync();
        }
    }
}
