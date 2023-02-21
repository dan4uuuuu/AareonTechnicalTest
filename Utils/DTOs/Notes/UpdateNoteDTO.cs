using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.DTOs.Notes
{
    public class UpdateNoteDTO
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string Comment { get; set; }
    }
}
