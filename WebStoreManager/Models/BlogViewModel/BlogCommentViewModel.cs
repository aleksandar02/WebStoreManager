using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStoreManager.Models.BlogViewModel
{
    public class BlogCommentViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int BlogId { get; set; }
        public string Comment { get; set; }
        public DateTime LastUpdate { get; set; }
        public DateTime Created { get; set; }

        public static BlogCommentDto MapFrom(BlogCommentViewModel blogCommentViewModel)
        {
            var blogCommentDto = new BlogCommentDto();

            blogCommentDto.Id = blogCommentViewModel.Id;
            blogCommentDto.UserName = blogCommentViewModel.UserName;
            blogCommentDto.BlogId = blogCommentViewModel.BlogId;
            blogCommentDto.Comment = blogCommentViewModel.Comment;
            blogCommentDto.LastUpdate = blogCommentViewModel.LastUpdate;
            blogCommentDto.Created = blogCommentViewModel.Created;

            return blogCommentDto;
        }

        public static BlogCommentViewModel MapTo(BlogCommentDto blogCommentDto)
        {
            var blogCommentViewModel = new BlogCommentViewModel();

            blogCommentViewModel.Id = blogCommentDto.Id;
            blogCommentViewModel.UserName = blogCommentDto.UserName;
            blogCommentViewModel.BlogId = blogCommentDto.BlogId;
            blogCommentViewModel.Comment = blogCommentDto.Comment;
            blogCommentViewModel.LastUpdate = blogCommentDto.LastUpdate;
            blogCommentViewModel.Created = blogCommentDto.Created;

            return blogCommentViewModel;
        }
    }
}
