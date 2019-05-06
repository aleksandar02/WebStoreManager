using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class ProductDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string CategoryDescription { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Barcode { get; set; }
        public string Image { get; set; }
        public DateTime Created { get; set; }
    }
}
