using HappyHours.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.Models.Signup
{
    public class SignupRequest : BaseRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string SystemEmail { get; set; }

        [Required]
        public string SystemPassword { get; set; }

        [Required]
        public string SystemNumber { get; set; }
    }
}
