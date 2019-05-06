using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class BlogPostDto
    {
        public int Id { get; set; }
        public string UserName{ get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string MainImage { get; set; }
        public bool Promote { get; set; }
        public DateTime Created { get; set; }
        public string CategoryDescription { get; set; }
    }
}
