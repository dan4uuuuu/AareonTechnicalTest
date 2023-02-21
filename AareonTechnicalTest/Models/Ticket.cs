using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AareonTechnicalTest.Models
{
    public class Ticket : BaseModel
    {
        public Ticket()
        {

        }
        [Key]
        public int Id { get; }

        public string Content { get; set; }

        public List<Note> Notes { get; set; }
    }
}
