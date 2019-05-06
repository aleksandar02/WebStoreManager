using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class BlogCommentDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int BlogId { get; set; }
        public string Comment { get; set; }
        public DateTime LastUpdate { get; set; }
        public DateTime Created { get; set; }

    }
}
