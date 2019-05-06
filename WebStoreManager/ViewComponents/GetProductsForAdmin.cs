using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStoreManager.Models.ProductViewModels;

namespace WebStoreManager.ViewComponents
{
    public class GetProductsForAdmin : ViewComponent
    {
        private readonly IConfiguration config;
        private readonly string _connectionString;
        private readonly ProductDal _productDal;

        public GetProductsForAdmin(IConfiguration config)
        {
            this.config = config;
            _connectionString = config["ConnectionStrings:ConfigurationDatabase"];
            _productDal = new ProductDal(_connectionString);
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var productsDto = await _productDal.GetAllProducts();
            var output = productsDto.Select(ProductViewModel.MapTo);

            return View(output);
        }
    }
}
