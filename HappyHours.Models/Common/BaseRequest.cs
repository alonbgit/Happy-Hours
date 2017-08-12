using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.Models.Common
{
    public class BaseRequest
    {
        [Required]
        public Credentials Credentials { get; set; }
    }
}
