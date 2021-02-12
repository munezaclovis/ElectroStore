using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroStore.Models
{
    public class Category
    {
        public string Id { set; get; } = Guid.NewGuid().ToString();
        public string Name { set; get; }
        public bool Deleted { set; get; } = false;
    }
}
