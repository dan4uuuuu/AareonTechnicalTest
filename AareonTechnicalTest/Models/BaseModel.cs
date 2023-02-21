using AareonTechnicalTest.Interfaces;
using System;

namespace AareonTechnicalTest.Models
{
    public class BaseModel : IAuditableEntity
    {
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set;}
    }
}
