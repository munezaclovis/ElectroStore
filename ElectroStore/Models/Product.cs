using System;
using System.Collections.Generic;
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
        public string Brand { set; get; }

        [Required]
        public string Categories { set; get; }

        [Required]
        public string DateAdded { set; get; }

        [Required]
        public string DateUpdated { set; get; }

        [Required]
        public string ImageURLs { set; get; }

        [Required]
        public string Name { set; get; }

        [Required]
        public string PrimaryCategories { set; get; }

        [Required]
        public string SourceURLs { set; get; }

        [Required]
        public string Upc { set; get; }

        [Required]
        public bool Deleted { get; set; } = false;
    }
}
