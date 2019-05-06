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
    public class GetAllBlogComments : ViewComponent
    {
        private readonly IConfiguration config;
        private readonly string _connectionString;
        private readonly BlogDal _blogDal;

        public GetAllBlogComments(IConfiguration config)
        {
            this.config = config;
            _connectionString = config["ConnectionStrings:ConfigurationDatabase"];
            _blogDal = new BlogDal(_connectionString);
        }

        // GET: Blog/GetAllBlogComments/5
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var blogCommentsDto = await _blogDal.GetAllCommentsAsync(id);

            var blogPostsModels = blogCommentsDto.Select(BlogCommentViewModel.MapTo);

            return View(blogPostsModels);
        }
    }
}
