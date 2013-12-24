using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NWindCorp2.bus.Entities
{
    public class Category
    {
        public Category()
        {
            _products = new List<Product>();
        }
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        private ICollection<Product> _products;
        public virtual ICollection<Product> Products
        {
            get { return _products; }
            set { _products = value; }
        }
    }
}
