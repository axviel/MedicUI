using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MedicDev.Models.UserProfile
{
    public class UpdateAccountModel
    {
        //[Required(ErrorMessage = "Username is required")]
        //public string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
    }
}
