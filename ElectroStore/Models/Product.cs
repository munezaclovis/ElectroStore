using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroStore.Models
{
    public class Product
    {
        [Required]
        public string Id { set; get; } = Guid.NewGuid().ToString();

        [Required]
        public string Name { set; get; }

        [Required]
        [DisplayName("Brand Name")]
        public string BrandId { set; get; }

        [Required]
        [DisplayName("Category Name")]
        public string CategoryId { set; get; }

        [Required]
        public string DateAdded { set; get; } = DateTime.UtcNow.ToString();

        [Required]
        public string DateUpdated { set; get; } = DateTime.UtcNow.ToString();

        [Required]
        [DisplayName("Image Urls")]
        public string ImageURLs { set; get; }

        [Required]
        public double Price { set; get; }

        [Required]
        public bool Deleted { set; get; } = false;

        public virtual Brand Brand { get; set; }

        public virtual Category Category { get; set; }

        public ICollection<CartItem> CartItems{ set; get; }
    }
}
