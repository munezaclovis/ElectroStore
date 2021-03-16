using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroStore.Models
{
    public class Cart
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        [Required]
        public string UserID { set; get; }

        public ICollection<CartItem> CartItems;
    }

    public class CartItem
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string ProductId { set; get; }

        [Required]
        public double Quantity { set; get; }

        [Required]
        public string CartId { get; set; } = Guid.NewGuid().ToString();

        public virtual Product Product { set; get; }

        public virtual Cart Cart { set; get; }
    }
}
