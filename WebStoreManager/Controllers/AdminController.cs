using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebStoreManager.Controllers
{
    public class AdminController : Controller
    {

        private readonly IConfiguration config;
        private readonly string _connectionString;
        private readonly ProductDal _productDal;

        public AdminController(IConfiguration config)
        {
            this.config = config;
            _connectionString = config["ConnectionStrings:ConfigurationDatabase"];
            _productDal = new ProductDal(_connectionString);
        }

        // GET: Admin
        public ActionResult Index(bool deletePostResult, bool editPostResult, bool deleteProductResult)
        {
            if(deletePostResult == true)
            {
                ViewBag.Message = "Post successfully deleted!";
            }

            if (editPostResult == true)
            {
                ViewBag.Message = "Post successfully edited!";
            }

            if (deleteProductResult == true)
            {
                ViewBag.Message = "Product successfully deleted!";
            }

            return View();
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}