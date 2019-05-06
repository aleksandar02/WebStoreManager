using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStoreManager.Models.PromotionViewModels;

namespace WebStoreManager.Models.ProductViewModels
{
    public class ProductViewModel
    {
        private readonly PromotionViewModel _productPromotion;

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
        public bool Promotion { get; set; }
        public DateTime PromotionLifetime { get; set; }
        public DateTime PromotionCreated { get; set; }
        public decimal PriceWithPromotion { get; set; }

        public ProductViewModel()
        {

        }

        public ProductViewModel(PromotionViewModel promotion)
        {
            _productPromotion = promotion;
            PromotionCreated = _productPromotion.Created;
            PromotionLifetime = _productPromotion.Lifetime;
            PriceWithPromotion = _productPromotion.Percent;
        }

        public static ProductViewModel MapTo(ProductDto productDto)
        {
            var product = new ProductViewModel();

            product.Id = productDto.Id;
            product.CategoryId = productDto.CategoryId;
            product.CategoryDescription = productDto.CategoryDescription;
            product.UserName = productDto.UserName;
            product.Description = productDto.Description.Length >= 85 ? $"{productDto.Description.Substring(0, 85)} ..." : productDto.Description; 
            product.Name = productDto.Name;
            product.Price = productDto.Price;
            product.Barcode = productDto.Barcode;
            product.Image = productDto.Image;
            product.Created = productDto.Created;

            return product;
        }

        public static ProductDto MapFrom(ProductViewModel product)
        {
            var productDto = new ProductDto();

            productDto.Id = product.Id;
            productDto.CategoryId = product.CategoryId;
            productDto.CategoryDescription = product.CategoryDescription;
            productDto.UserName = product.UserName;
            productDto.Description = product.Description;
            productDto.Name = product.Name;
            productDto.Price = product.Price;
            productDto.Barcode = product.Barcode;
            productDto.Image = product.Image;
            productDto.Created = product.Created;

            return productDto;
        }
    }
}
