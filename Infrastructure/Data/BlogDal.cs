using Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace Infrastructure.Data
{
    public class BlogDal
    {
        private readonly string _connectionString;

        public BlogDal(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public bool InsertBlogPost(BlogPostDto blogPostDto)
        {
            bool result = true;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sqlProcedure = "InsertBlogPost";
                    SqlCommand command = new SqlCommand(sqlProcedure, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserName", blogPostDto.UserName);
                    command.Parameters.AddWithValue("@CategoryId", blogPostDto.CategoryId);
                    command.Parameters.AddWithValue("@Title", blogPostDto.Title);
                    command.Parameters.AddWithValue("@Description", blogPostDto.Description);
                    command.Parameters.AddWithValue("@Content", blogPostDto.Content);
                    command.Parameters.AddWithValue("@MainImage", blogPostDto.MainImage);
                    command.Parameters.AddWithValue("@Promote", blogPostDto.Promote);
                    command.Parameters.AddWithValue("@Created", blogPostDto.Created);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                result = false;
            }

            return result;
        }

        public async Task<IEnumerable<BlogPostDto>> GetAllBlogPosts()
        {
            var posts = new List<BlogPostDto>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sqlProcedure = "GetAllBlogPosts";
                    SqlCommand command = new SqlCommand(sqlProcedure, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync().ConfigureAwait(false))
                        {
                            var postDto = new BlogPostDto();

                            postDto = MapToBlogPost(reader);

                            posts.Add(postDto);
                        }
                    }                   
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }

            return posts;
        }

        public async Task<BlogPostDto> GetBlogPostById(int id)
        {
            var postDto = new BlogPostDto();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sqlProcedure = "GetBlogPostById";
                    SqlCommand command = new SqlCommand(sqlProcedure, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync().ConfigureAwait(false))
                        {
                            postDto = MapToBlogPost(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }

            return postDto;
        }

        public async Task<IEnumerable<BlogPostDto>> GetRecomendedBlogs(int id)
        {
            var output = new List<BlogPostDto>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sqlProcedure = "GetRecomendedBlogs";
                    SqlCommand command = new SqlCommand(sqlProcedure, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync().ConfigureAwait(false))
                        {
                            var blogPostDto = new BlogPostDto();
                            blogPostDto = MapToBlogPost(reader);

                            output.Add(blogPostDto);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }

            return output;
        }

        public bool EditBlogPost(BlogPostDto blogPostDto)
        {
            bool result = true;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sqlProcedure = "EditBlogPost";
                    SqlCommand command = new SqlCommand(sqlProcedure, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", blogPostDto.Id);
                    command.Parameters.AddWithValue("@CategoryId", blogPostDto.CategoryId);
                    command.Parameters.AddWithValue("@UserName", blogPostDto.UserName);
                    command.Parameters.AddWithValue("@Title", blogPostDto.Title);
                    command.Parameters.AddWithValue("@Content", blogPostDto.Content);
                    command.Parameters.AddWithValue("@Description", blogPostDto.Description);
                    command.Parameters.AddWithValue("@MainImage", blogPostDto.MainImage);
                    command.Parameters.AddWithValue("@Promote", blogPostDto.Promote);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                result = false;
            }

            return result;
        }

        public bool DeleteBlogPost(int id)
        {
            bool result = true;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sqlProcedure = "DeleteBlogPost";
                    SqlCommand command = new SqlCommand(sqlProcedure, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                result = false;
            }

            return result;
        }

        public bool CreateBlogComment(BlogCommentDto blogCommentDto)
        {
            bool result = true;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sqlProcedure = "InsertBlogComment";
                    SqlCommand command = new SqlCommand(sqlProcedure, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@BlogId", blogCommentDto.BlogId);
                    command.Parameters.AddWithValue("@UserName", blogCommentDto.UserName);
                    command.Parameters.AddWithValue("@Comment", blogCommentDto.Comment);
                    command.Parameters.AddWithValue("@LastUpdate", blogCommentDto.LastUpdate);
                    command.Parameters.AddWithValue("@Created", blogCommentDto.Created);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                result = false;
            }

            return result;
        }

        public bool DeletePostComment(int id)
        {
            bool result = true;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sqlProcedure = "DeletePostComment";
                    SqlCommand command = new SqlCommand(sqlProcedure, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", id);                    

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                result = false;
            }

            return result;
        }

        public bool UpdatePostComment(BlogCommentDto postCommentDto)
        {
            bool result = true;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sqlProcedure = "UpdatePostComment";
                    SqlCommand command = new SqlCommand(sqlProcedure, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", postCommentDto.Id);
                    command.Parameters.AddWithValue("@Comment", postCommentDto.Comment);
                    command.Parameters.AddWithValue("@LastUpdate", postCommentDto.LastUpdate);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                result = false;
            }

            return result;
        }

        public BlogCommentDto GetPostCommentById(int id)
        {
            var postComment = new BlogCommentDto();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sqlProcedure = "GetPostCommentById";
                    SqlCommand command = new SqlCommand(sqlProcedure, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            postComment = MapToBlogCommentDto(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }

            return postComment;
        }

        public async Task<IEnumerable<BlogCommentDto>> GetAllCommentsAsync(int id)
        {
            var blogComments = new List<BlogCommentDto>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sqlProcedure = "GetAllBlogComments";
                    SqlCommand command = new SqlCommand(sqlProcedure, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync().ConfigureAwait(false))
                        {
                            var commentDto = new BlogCommentDto();

                            commentDto = MapToBlogCommentDto(reader);

                            blogComments.Add(commentDto);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }

            return blogComments;
        }

        private BlogCommentDto MapToBlogCommentDto(SqlDataReader reader)
        {
            var blogComment = new BlogCommentDto();

            blogComment.Id = Convert.ToInt32(reader["Id"]);
            blogComment.BlogId = Convert.ToInt32(reader["BlogId"]);
            blogComment.UserName = reader["UserName"].ToString();
            blogComment.Comment = reader["Comment"].ToString();
            blogComment.LastUpdate = Convert.ToDateTime(reader["LastUpdate"]);
            blogComment.Created = Convert.ToDateTime(reader["Created"]);

            return blogComment;
        }

        public BlogPostDto MapToBlogPost(SqlDataReader reader)
        {
            var post = new BlogPostDto();

            post.Id = Convert.ToInt32(reader["Id"]);
            post.CategoryId = Convert.ToInt32(reader["CategoryId"]);
            post.CategoryDescription = reader["CategoryDescription"].ToString();
            post.UserName = reader["UserName"].ToString();
            post.Title = reader["Title"].ToString();
            post.Description = reader["Description"].ToString();
            post.Content = reader["Content"].ToString();
            post.MainImage = reader["MainImage"].ToString();
            post.Promote = Convert.ToBoolean(reader["Promote"]);
            post.Created = Convert.ToDateTime(reader["Created"]);

            return post;
        }
    }
}