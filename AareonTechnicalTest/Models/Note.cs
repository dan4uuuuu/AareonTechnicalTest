using System.ComponentModel.DataAnnotations;

namespace AareonTechnicalTest.Models
{
    public class Note
    {
        [Key]
        public int Id { get; }

        public string Comment { get; set; }

        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }
}
