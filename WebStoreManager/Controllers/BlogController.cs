using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WebStoreManager.Models.BlogViewModel;

namespace WebStoreManager.Controllers
{
    public class BlogController : Controller
    {
        private readonly IConfiguration config;
        private readonly string _connectionString;
        private readonly BlogDal _blogDal;

        public BlogController(IConfiguration config)
        {
            this.config = config;
            _connectionString = config["ConnectionStrings:ConfigurationDatabase"];
            _blogDal = new BlogDal(_connectionString);
        }

        //GET: Blog
        public async Task<IActionResult> Index(bool createPost)
        {
            IEnumerable<BlogPostDto> blogPostsDto = new List<BlogPostDto>();

            blogPostsDto = await _blogDal.GetAllBlogPosts();

            var blogPostsModels = blogPostsDto.Select(BlogPostViewModel.MapTo);

            if(createPost == true)
            {
                ViewBag.Message = "Successfully created!";
            }

            return View(blogPostsModels);
        }

        // GET: Blog/Details/5
        public async Task<IActionResult> DetailsAsync(int id, bool deleteComment, bool addComment, bool editComment)
        {
            var blogPostDto = await _blogDal.GetBlogPostById(id);

            var blogPostModel = BlogPostViewModel.MapTo(blogPostDto);

            if(deleteComment == true)
            {
                ViewBag.Message = "Successfully deleted!";
            }

            if(addComment == true)
            {
                ViewBag.Message = "Commented successfully!";
            }

            if (editComment == true)
            {
                ViewBag.Message = "Edited successfully!";
            }


            return View("Details", blogPostModel);
        }

        // GET: Blog/Create
        [Authorize(Roles = "Blogger, SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Blog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Blogger, SuperAdmin")]
        public async Task<IActionResult> CreateAsync(IFormCollection collection)
        {
            try
            {
                var imageFile = Request?.Form.Files[0];

                if (imageFile != null && imageFile.Length > 0 && imageFile.ContentType.Contains("image"))
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/blog-images", imageFile.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                }

                var blogPostDto = new BlogPostDto();

                blogPostDto.CategoryId = Convert.ToInt32(collection["CategoryId"]);
                blogPostDto.UserName = User.Identity.Name;
                blogPostDto.Title = collection["Title"];
                blogPostDto.Description = collection["Description"];
                blogPostDto.Content = collection["Content"];
                blogPostDto.Created = DateTime.Now;
                blogPostDto.MainImage = string.IsNullOrEmpty(imageFile.FileName) ? string.Empty : $"{Request.Scheme}://{Request.Host}/images/blog-images/{imageFile.FileName}";
                blogPostDto.Promote = Convert.ToBoolean(collection["Promote"]);

                bool result = _blogDal.InsertBlogPost(blogPostDto);

                return RedirectToAction(nameof(Index), new { createPost = result });
            }

            catch (Exception ex)
            {
                return View("Index");
            }
        }

        // GET: Blog/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: Blog/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> EditAsync(IFormCollection collection)
        {
            try
            {
                var imageFile = Request?.Form.Files[0];

                if (imageFile != null && imageFile.Length > 0 && imageFile.ContentType.Contains("image"))
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/blog-images", imageFile.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                }

                var blogPostDto = new BlogPostDto();

                blogPostDto.Id = Convert.ToInt32(collection["editPostId"]);
                blogPostDto.Title = collection["editTitle"].ToString();
                blogPostDto.Content = collection["editContent"].ToString();
                blogPostDto.Description = collection["editDescription"].ToString();
                blogPostDto.CategoryId = Convert.ToInt32(collection["editCategoryId"]);
                blogPostDto.UserName = User.Identity.Name;
                blogPostDto.Promote = Convert.ToBoolean(collection["editPromote"]);
                blogPostDto.MainImage = string.IsNullOrEmpty(imageFile.FileName) ? string.Empty : $"{Request.Scheme}://{Request.Host}/images/blog-images/{imageFile.FileName}";

                var result = _blogDal.EditBlogPost(blogPostDto);

                return RedirectToAction("Index", "Admin", new { editPostResult = result });
            }
            catch(Exception ex)
            {
                return View("Index", "Admin");
            }
        }

        // GET: Blog/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: Blog/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(IFormCollection collection)
        {
            try
            {
                int id = Convert.ToInt32(collection["deletePostId"]);
                bool result = _blogDal.DeleteBlogPost(id);

                return RedirectToAction("Index", "Admin", new { deleteResult = result });
            }
            catch
            {
                return View();
            }
        }

        [Authorize]
        public IActionResult CreateBlogCommentAsync(IFormCollection collection)
        {
            try
            {
                var blogCommentDto = new BlogCommentDto();

                blogCommentDto.BlogId = Convert.ToInt32(collection["blogId"]);
                blogCommentDto.UserName = User.Identity.Name;
                blogCommentDto.Comment = collection["blogComment"];
                blogCommentDto.LastUpdate = DateTime.Now;
                blogCommentDto.Created = DateTime.Now;

                bool result = _blogDal.CreateBlogComment(blogCommentDto);



                return Redirect("/Blog/DetailsAsync/" + blogCommentDto.BlogId + "?addComment=" + result);
            }

            catch (Exception ex)
            {
                return View("Index");
            }
        }

        // POST: Blog/Edit/5
        [HttpPost]
        [Authorize]
        public IActionResult DeletePostComment(IFormCollection collection)
        {
            try
            {
                bool result = false;
                int id = Convert.ToInt32(collection["commentId"]);

                var postComment = _blogDal.GetPostCommentById(id);
                if(postComment.UserName == User.Identity.Name || User.IsInRole("SuperAdmin") == true)
                {
                    result = _blogDal.DeletePostComment(id);
                }

                return RedirectToAction("DetailsAsync", new { id = postComment.BlogId, deleteComment = result});
            }
            catch
            {
                return View();
            }
        }

        [Authorize]
        public IActionResult EditPostComment(IFormCollection collection)
        {
            var postComment = _blogDal.GetPostCommentById(Convert.ToInt32(collection["editCommentId"]));

            postComment.Comment = collection["postComment"];
            postComment.LastUpdate = DateTime.Now;

            bool result = _blogDal.UpdatePostComment(postComment);

            return RedirectToAction("DetailsAsync", new { id = postComment.BlogId, editComment = result });
        }

        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> GetAllPosts()
        {
            var blogPostsDto = await _blogDal.GetAllBlogPosts();
            var output = blogPostsDto.Select(BlogPostViewModel.MapTo);

            return Json(output);
        }

    }
}