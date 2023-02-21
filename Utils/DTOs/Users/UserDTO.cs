using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.DTOs.Users
{
    public class UserDTO
    {
        public string FirtsName { get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool isAdmin { get; set; }
    }
}
