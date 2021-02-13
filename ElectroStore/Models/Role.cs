using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroStore.Models
{
    public class Role : IdentityRole
    {
        public Role(string name) : base(name) { }
        public ICollection<UserRole> UsersRoles { get; set; }
    }
}
