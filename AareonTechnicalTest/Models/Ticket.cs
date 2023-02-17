using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AareonTechnicalTest.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; }

        public string Content { get; set; }

        public int PersonId { get; set; }

        public int NoteId { get; set; }
        public List<Note> Notes { get; set; }
    }
}
