using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.Logic.Models
{
    public class User
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string SystemEmail { get; set; }

        public string SystemPassword { get; set; }

        public string SystemNumber { get; set; }

        public bool IsEmailVerified { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
