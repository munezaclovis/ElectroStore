using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroStore.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string Firstname { set; get; }

        [Required]
        public string Lastname { set; get; }

        [Required]
        public string Status { set; get; } = "A";

        public ICollection<UserRole> UsersRoles { get; set; }
    }
}
