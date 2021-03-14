using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroStore.Models
{
    public class Category
    {
        [Required]
        public string Id { set; get; } = Guid.NewGuid().ToString();

        [Required]
        public string Name { set; get; }

        [Required]
        public string Icon { set; get; } = "";

        [Required]
        public bool Deleted { set; get; } = false;

        public ICollection<Product> Products { get; set; }
    }
}
