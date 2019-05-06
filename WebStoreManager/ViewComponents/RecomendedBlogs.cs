using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStoreManager.Models.BlogViewModel;
using Infrastructure.Data;

namespace WebStoreManager.ViewComponents
{
    public class RecomendedBlogs : ViewComponent
    {
        private readonly IConfiguration config;
        private readonly string _connectionString;
        private readonly BlogDal _blogDal;

        public RecomendedBlogs(IConfiguration config)
        {
            this.config = config;
            _connectionString = config["ConnectionStrings:ConfigurationDatabase"];
            _blogDal = new BlogDal(_connectionString);
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var blogPostsDto = await _blogDal.GetRecomendedBlogs(id);

            var blogPostsModels = blogPostsDto.Select(BlogPostViewModel.MapTo);

            return View(blogPostsModels);
        }
    }
}
