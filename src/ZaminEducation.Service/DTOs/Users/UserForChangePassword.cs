using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZaminEducation.Service.DTOs.Users
{
    public class UserForChangePassword
    {
        [Required(ErrorMessage = "Value must not be null or empty!")]
        public string Username { get; set; }


        [Required(ErrorMessage = "Old password must not be null or empty!")]
        public string OldPassword { get; set; }


        [Required(ErrorMessage = "New password must not be null or empty!")]
        public string NewPassword { get; set; }

        public string ComfirmPassword { get; set; }
    }
}
