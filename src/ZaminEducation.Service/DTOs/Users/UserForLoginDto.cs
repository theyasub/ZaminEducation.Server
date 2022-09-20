using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZaminEducation.Service.DTOs.Users
{
    public class UserForLoginDto
    {
        [Required]
        [MaxLength(20)]
        public string Login { get; set; }

        [Required]
        [MaxLength(20)]
        public string Password { get; set; }
    }
}