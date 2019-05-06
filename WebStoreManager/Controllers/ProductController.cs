using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebStoreManager.Models.ProductViewModels;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Core.Entities;
using WebStoreManager.Models.PromotionViewModels;

namespace WebStoreManager.Controllers
{
    public class ProductController : Controller
    {
        private readonly IConfiguration config;
        private readonly string _connectionString;
        private readonly ProductDal _productDal;

        public ProductController(IConfiguration config)
        {
            this.config = config;
            _connectionString = config["ConnectionStrings:ConfigurationDatabase"];
            _productDal = new ProductDal(_connectionString);
        }


        // GET: Product
        [HttpGet]
        public IActionResult Index(bool insertProduct)
        {
            if(insertProduct == true)
            {
                ViewBag.Message = "Successfully created!";
            }

            return View();
        }

        public async Task<IActionResult> GetProducts()
        {
            var productsDto = await _productDal.GetAllProducts();
            var productsModel = productsDto.Select(ProductViewModel.MapTo);

            string productsJson = JsonConvert.SerializeObject(productsModel);

            return Content(productsJson);
        }

        // GET: Product/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateAsync(IFormCollection collection)
        {
            try
            {
                var imageFile = Request?.Form.Files[0];

                if (imageFile != null && imageFile.Length > 0 && imageFile.ContentType.Contains("image"))
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/product-images", imageFile.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                }

                var productModel = new ProductViewModel();
                var promotionModel = new PromotionViewModel();

                productModel.Name = collection["Name"].ToString();
                productModel.Description = collection["Description"].ToString();
                productModel.CategoryId = Convert.ToInt32(collection["CategoryId"]);
                productModel.Price = Convert.ToDecimal(collection["Price"]);
                productModel.Barcode = collection["Barcode"].ToString();
                productModel.UserName = User.Identity.Name;
                productModel.Created = DateTime.Now;
                productModel.Image = string.IsNullOrEmpty(imageFile.FileName) ? string.Empty : $"{Request.Scheme}://{Request.Host}/images/product-images/{imageFile.FileName}";

                promotionModel.Created = DateTime.Now;
                promotionModel.Lifetime = Convert.ToDateTime(collection["PromotionLifetime"]);
                promotionModel.Percent = Convert.ToDecimal(collection["PromotionPercent"]);

                var productDto = ProductViewModel.MapFrom(productModel);

                bool insertProductResult = _productDal.InsertProduct(productDto);

                if (insertProductResult == true && productModel.Promotion == true)
                {
                   // TODO: Insert promotion
                }

                return RedirectToAction(nameof(Index), new { insertProduct = insertProductResult });
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return View();
            }
        }

        // GET: Product/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(IFormCollection collection)
        {
            try
            {
                var imageFile = Request?.Form.Files[0];

                if (imageFile != null && imageFile.Length > 0 && imageFile.ContentType.Contains("image"))
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/product-images", imageFile.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                }

                var productDto = new ProductDto();

                productDto.Id = Convert.ToInt32(collection["editProductId"]);
                productDto.Name = collection["editName"].ToString();
                productDto.Price = Convert.ToDecimal(collection["editPrice"]);
                productDto.Barcode = collection["editBarcode"].ToString();
                productDto.UserName = User.Identity.Name;
                productDto.CategoryId = Convert.ToInt32(collection["editCategoryId"]);
                productDto.Description = collection["editDescription"].ToString();
                productDto.Image = string.IsNullOrEmpty(imageFile.FileName) ? string.Empty : $"{Request.Scheme}://{Request.Host}/images/product-images/{imageFile.FileName}";

                var result = _productDal.EditProduct(productDto);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(IFormCollection collection)
        {
            try
            {
                int id = Convert.ToInt32(collection["deleteProductId"]);
                bool result = _productDal.DeleteProduct(id);

                return RedirectToAction("Index", "Admin", new { deleteProductResult = result });
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> SearchProducts(string search)
        {
            search = search as string ?? string.Empty;

            var productDtos = await _productDal.SearchProducts(search.Trim());
            var productViewModels= productDtos.Select(ProductViewModel.MapTo);

            string output = JsonConvert.SerializeObject(productViewModels);

            return Content(output);
        }

        public async Task<IActionResult> GetAllProducts()
        {
            var productsDto = await _productDal.GetAllProducts();
            var output = productsDto.Select(ProductViewModel.MapTo);

            return Json(output);
        }
    }
}