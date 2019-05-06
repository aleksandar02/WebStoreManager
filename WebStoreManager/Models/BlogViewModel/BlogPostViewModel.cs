using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStoreManager.Models.BlogViewModel
{
    public class BlogPostViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryDescription { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string MainImage { get; set; }
        public bool Promote { get; set; }
        public DateTime Created { get; set; }        

        public static BlogPostDto MapFrom(BlogPostViewModel blogPostViewModel)
        {
            var blogPostDto = new BlogPostDto();

            blogPostDto.Id = blogPostViewModel.Id;
            blogPostDto.UserName = blogPostViewModel.UserName;
            blogPostDto.CategoryId = blogPostViewModel.CategoryId;
            blogPostDto.CategoryDescription = blogPostViewModel.CategoryDescription;
            blogPostDto.Title = blogPostViewModel.Title;
            blogPostDto.Description = blogPostViewModel.Description;
            blogPostDto.Content = blogPostViewModel.Content;
            blogPostDto.MainImage = blogPostViewModel.MainImage;
            blogPostDto.Promote = blogPostViewModel.Promote;
            blogPostDto.Created = blogPostViewModel.Created;

            return blogPostDto;
        }

        public static BlogPostViewModel MapTo(BlogPostDto blogPostDto)
        {
            var blogPostViewModel = new BlogPostViewModel();

            blogPostViewModel.Id = blogPostDto.Id;
            blogPostViewModel.UserName = blogPostDto.UserName;
            blogPostViewModel.CategoryId = blogPostDto.CategoryId;
            blogPostViewModel.CategoryDescription = blogPostDto.CategoryDescription;
            blogPostViewModel.Title = blogPostDto.Title;
            blogPostViewModel.Description = blogPostDto.Description.Length >= 300 ?  $"{blogPostDto.Description.Substring(0, 300)} ..." : blogPostDto.Description;
            blogPostViewModel.Content = blogPostDto.Content;
            blogPostViewModel.MainImage = blogPostDto.MainImage;
            blogPostViewModel.Promote = blogPostDto.Promote;
            blogPostViewModel.Created = blogPostDto.Created;

            return blogPostViewModel;
        }
    }
}
