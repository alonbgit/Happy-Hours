using HappyHours.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.Models.Signup
{
    public class CheckEmailExistRequest : BaseRequest
    {
        [Required]
        public string Email { get; set; }
    }
}
