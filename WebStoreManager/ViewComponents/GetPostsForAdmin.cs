using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStoreManager.Models.BlogViewModel;

namespace WebStoreManager.ViewComponents
{
    public class GetPostsForAdmin : ViewComponent
    {
        private readonly IConfiguration config;
        private readonly string _connectionString;
        private readonly BlogDal _blogDal;

        public GetPostsForAdmin(IConfiguration config)
        {
            this.config = config;
            _connectionString = config["ConnectionStrings:ConfigurationDatabase"];
            _blogDal = new BlogDal(_connectionString);
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var blogPostsDto = await _blogDal.GetAllBlogPosts();
            var output = blogPostsDto.Select(BlogPostViewModel.MapTo);

            return View(output);
        }


    }
}
