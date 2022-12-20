using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using shop.entity;

namespace Shop.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Kategori ad� zorunludur.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Kategori i�in 5-100 aras�nda karakter giriniz.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Url zorunludur.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Url i�in 5-100 aras�nda karakter giriniz.")]
        public string Url { get; set; }

        public List<Product>? Products { get; set; }

        

    }
}