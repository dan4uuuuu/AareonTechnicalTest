using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Exceptions
{
    public class ErrorResponse
    {
        public ErrorResponse(string error, List<string> args = null, int status = 0)
        {
            this.Errors.Add(error, args);
            this.Status = status;
        }

        public Dictionary<string, List<string>> Errors { get; } = new();

        public int Status { get; set; }
    }
}
