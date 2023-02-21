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
using System.Web.Http.Results;
using Utils.DTOs.Notes;
using Utils.Extensions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AareonTechnicalTest.Controllers
{
    [Authorize]
    [ApiController]
    public class NotesController : BaseController
    {
        private readonly ILogger _logger;
        private readonly UserManager<Person> _userManager;
        public NotesController(ApplicationContext context, ILogger<NotesController> logger, UserManager<Person> userManager) : base(context)
        {
            _logger= logger;
            _userManager = userManager;
        }
        // GET: api/<TicketController>
        [HttpGet]
        [Route("GetAllNotes")]
        public async Task<List<Note>> GetAllNotes()
        {
            List<Note> list = new List<Note>();
            list = await this.Context.Notes.ToListAsync();
            this._logger.LogInformation("Returned All notes");
            return list;
        }

        // GET api/<TicketController>/5
        [HttpGet]
        [Route("GetAllNotesById")]
        public async Task<Note> GetAllNotesById(int id)
        {
            this._logger.LogInformation("Returned note by ID: " + id);
            return await this.Context.Notes.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        // POST api/<TicketController>
        [HttpPost]
        [Route("CreateNote")]
        public async Task<int> CreateNote(CreateNoteDTO note)
        {
            Ticket ticket = await this.Context.Tickets.Where(x => x.Id == note.TicketId).FirstOrDefaultAsync();
            if(ticket != null)
            {
                this.Context.Notes.Add(
                new Note()
                {
                    TicketId = note.TicketId,
                    Comment = note.Comment,
                    CreatedBy = this.User.GetUserId(),
                    UpdatedBy = this.User.GetUserId(),
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsSuspended = false
                });
                
                int result = await this.Context.SaveChangesAuditableAsync(this.User.GetUserId());
                if(result > 0)
                {
                    this._logger.LogInformation("Created Note to ticket with ID: " + note.TicketId);
                    return result;
                }
                else
                {
                    this._logger.LogInformation("Note not created");
                    return 0;
                }
            }
            else
            {
                this._logger.LogInformation("Ticket with ID: " + note.TicketId + " not found");
                return 0;
            }
            
        }

        // PUT api/<TicketController>/5
        [HttpPut]
        [Route("UpdateNote")]
        [Authorize(Roles = "Admin")]
        public async Task<int> UpdateNote(UpdateNoteDTO note)
        {
            Note dbNote = this.Context.Notes.Where(x => x.Id== note.Id).FirstOrDefault();
            if (dbNote != null) 
            {
                Ticket ticket = await this.Context.Tickets.Where(x => x.Id == dbNote.TicketId).FirstOrDefaultAsync();
                if (ticket != null)
                {
                    dbNote.Comment = note.Comment;
                    dbNote.TicketId = note.TicketId;
                    dbNote.UpdatedBy = new System.Guid(_userManager.GetUserId(HttpContext.User));
                    dbNote.UpdatedDate = DateTime.Now;
                    dbNote.IsSuspended = false;

                    int result = await this.Context.SaveChangesAuditableAsync(this.User.GetUserId());
                    if (result > 0)
                    {
                        this._logger.LogInformation("Updated Note to ticket with ID: " + note.TicketId);
                        return result;
                    }
                    else
                    {
                        this._logger.LogInformation("Note not updated");
                        return 0;
                    }
                }
                else
                {
                    this._logger.LogInformation("Ticket with ID: " + note.TicketId + " not found");
                    return 0;
                }
            }
            else
            {
                this._logger.LogInformation("Note with ticket ID : " + note.TicketId + " not found");
                return 0;
            }
        }
        // PUT api/<TicketController>/5
        [HttpPut]
        [Route("RemoveNote")]
        public async Task<int> RemoveNote(RemoveNoteDTO note)
        {
            Note dbNote = this.Context.Notes.Where(x => x.Id == note.NoteId).FirstOrDefault();
            if (dbNote != null)
            {
                dbNote.UpdatedBy = new System.Guid(_userManager.GetUserId(HttpContext.User));
                dbNote.UpdatedDate = DateTime.Now;
                dbNote.IsSuspended = true;
                
                int result = await this.Context.SaveChangesAuditableAsync(this.User.GetUserId());
                if (result > 0)
                {
                    this._logger.LogInformation("Suspended note with ID: " + note.NoteId);
                    return result;
                }
                else
                {
                    this._logger.LogInformation("Note not removed");
                    return 0;
                }
            }
            else
            {
                this._logger.LogInformation("Note with ID: " + note.NoteId + " not found");
                return 0;
            }

        }

        // DELETE api/<TicketController>/5
        [HttpDelete]
        [Route("DeleteNote")]
        [Authorize(Roles = "Admin")]
        public async Task<int> DeleteNote(int id)
        {
            var note = await this.Context.Notes.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (note != null)
            {
                this.Context.Remove<Note>(note);
                
                int result = await this.Context.SaveChangesAuditableAsync(this.User.GetUserId());
                if (result > 0)
                {
                    this._logger.LogInformation("Deleted Note with ID: " + note.Id);
                    return result;
                }
                else
                {
                    this._logger.LogInformation("Note not deleted");
                    return 0;
                }
            }
            else
            {
                this._logger.LogInformation("Note with ID: " + id + " not found");
                return 0;
            }
            
        }
    }
}
