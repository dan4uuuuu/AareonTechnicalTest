using AareonTechnicalTest.Controllers.Utils;
using AareonTechnicalTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AareonTechnicalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : BaseController
    {
        public NotesController(ApplicationContext context) : base(context)
        {

        }
        // GET: api/<TicketController>
        [HttpGet]
        public async Task<List<Note>> Get()
        {
            List<Note> list = new List<Note>();
            list = await this.Context.Notes.ToListAsync();
            return list;
        }

        // GET api/<TicketController>/5
        [HttpGet("{id}")]
        public async Task<Note> Get(int id)
        {
            return await this.Context.FindAsync<Note>(id);
        }

        // POST api/<TicketController>
        [HttpPost]
        public async Task<int> Post(Note note)
        {
            this.Context.Notes.Add(note);
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
            var note = await this.Context.FindAsync<Note>(id);
            this.Context.Remove<Note>(note);
            return await this.Context.SaveChangesAsync();
        }
    }
}
