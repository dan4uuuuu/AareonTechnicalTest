using AareonTechnicalTest.Controllers.Utils;
using AareonTechnicalTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils.DTOs.Tickets;
using Utils.Extensions;

namespace AareonTechnicalTest.Controllers
{
    [Authorize]
    public class TicketController : BaseController
    {
        private readonly ILogger _logger;
        private readonly UserManager<Person> _userManager;
        public TicketController(ApplicationContext context, ILogger<TicketController> logger, UserManager<Person> userManager) : base(context)
        {
            _logger = logger;
            _userManager = userManager;
        }
        // GET: api/<TicketController>
        [HttpGet]
        [Route("GetAllTickets")]
        public async Task<List<Ticket>> GetAllTickets()
        {
            this._logger.LogInformation("Returned all tickets");
            return await this.Context.Tickets.Include(x => x.Notes).ToListAsync(); ;
        }

        // GET api/<TicketController>/5
        [HttpGet]
        [Route("GetAllTicketsById")]
        public async Task<Ticket> GetAllTicketsById(int id)
        {
            this._logger.LogInformation("Returned ticket with ID: " + id);
            return await this.Context.Tickets.Where(x => x.Id == id).Include(x => x.Notes).FirstOrDefaultAsync();
        }

        // POST api/<TicketController>
        [HttpPost]
        [Route("CreateTicket")]
        public async Task<int> CreateTicket(TicketDTO ticket)
        {
            this.Context.Tickets.Add(
                new Ticket()
                {
                    Content = ticket.Content,
                    CreatedBy = this.User.GetUserId(),
                    UpdatedBy = this.User.GetUserId(),
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                });
            
            int result = await this.Context.SaveChangesAuditableAsync(this.User.GetUserId());
            if (result > 0)
            {
                this._logger.LogInformation("Created Ticket");
                return result;
            }
            else
            {
                this._logger.LogInformation("Ticket not created");
                return 0;
            }

        }

        // PUT api/<TicketController>/5
        [HttpPut]
        [Route("UpdateTicket")]
        public async Task<int> UpdateTicket(TicketDTO ticket)
        {
            Ticket dbTicket = this.Context.Tickets.Where(x => x.Id == ticket.Id).FirstOrDefault();
            if (dbTicket == null)
            {
                dbTicket.UpdatedDate = DateTime.Now;
                dbTicket.Content = ticket.Content;
                dbTicket.UpdatedBy = this.User.GetUserId();
                int result = await this.Context.SaveChangesAuditableAsync(this.User.GetUserId());
                if (result > 0)
                {
                    this._logger.LogInformation("Updated Ticket - ID: " + dbTicket.Id);
                    return result;
                }
                else
                {
                    this._logger.LogInformation("Ticket not updated");
                    return 0;
                }
            }
            else
            {
                this._logger.LogInformation("Ticket not found");
                return 0;
            }
        }

        // DELETE api/<TicketController>/5
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("DeleteTicket")]
        public async Task<int> DeleteTicket(int id)
        {
            var ticket = await this.Context.Tickets.Where(x => x.Id == id).FirstOrDefaultAsync();
            if(ticket != null)
            {
                this.Context.Remove(ticket);

                int result = await this.Context.SaveChangesAuditableAsync(this.User.GetUserId());
                if (result > 0)
                {
                    this._logger.LogInformation("Deleted Ticket - ID: " + id);
                    return result;
                }
                else
                {
                    this._logger.LogInformation("Ticket not deleted");
                    return 0;
                }
            }
            else
            {
                this._logger.LogInformation("Ticket not found");
                return 0;
            }
            
        }
    }
}
