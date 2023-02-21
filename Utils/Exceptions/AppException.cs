using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Exceptions
{
    public class AppException : Exception
    {
        public AppException(string message) : base(message) { }

        public AppException(string message, List<string> args) : base(message)
        {
            this.Args = args;
        }

        public List<string> Args { get; set; } = new();
    }
}
