using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.DTOs.Notes
{
    public class CreateNoteDTO
    {
        public CreateNoteDTO() { }
        public string Comment { get; set; }
        public int TicketId { get; set; }
        public bool IsSuspended { get; set; } = false;
    }
}
