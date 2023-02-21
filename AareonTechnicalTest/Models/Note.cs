using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AareonTechnicalTest.Models
{
    public class Note : BaseModel
    {
        [Key]
        public int Id { get; }

        public string Comment { get; set; }

        [ForeignKey("Ticket")]
        public int TicketId { get; set; }

        public bool IsSuspended { get; set; }
    }
}
