using System.Collections.Generic;
using shop.entity;


namespace Shop.Models
{
    public class ProductDetailModel
    {
        public Product? Product { get; set; }
        public List<Category>? Categories { get; set; }
    }
}