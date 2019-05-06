using Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductDal
    {
        private readonly string _connectionString;

        public ProductDal(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            var products = new List<ProductDto>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sqlProcedure = "GetAllProducts";
                    SqlCommand command = new SqlCommand(sqlProcedure, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync().ConfigureAwait(false))
                        {
                            var product = new ProductDto();

                            product = MapToProductDto(reader);

                            products.Add(product);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }


            return products;
        }

        public bool InsertProduct(ProductDto product)
        {
            bool result = true;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sqlProcedure = "InsertProduct";
                    SqlCommand command = new SqlCommand(sqlProcedure, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                    command.Parameters.AddWithValue("@UserName", product.UserName);
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Description", product.Description);
                    command.Parameters.AddWithValue("@Image", product.Image);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@Created", product.Created);
                    command.Parameters.AddWithValue("@Barcode", product.Barcode);

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

        public bool DeleteProduct(int id)
        {
            bool result = true;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sqlProcedure = "DeleteProduct";
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

        public bool EditProduct(ProductDto productDto)
        {

            bool result = true;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sqlProcedure = "EditProduct";
                    SqlCommand command = new SqlCommand(sqlProcedure, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", productDto.Id);
                    command.Parameters.AddWithValue("@CategoryId", productDto.CategoryId);
                    command.Parameters.AddWithValue("@UserName", productDto.UserName);
                    command.Parameters.AddWithValue("@Name", productDto.Name);
                    command.Parameters.AddWithValue("@Description", productDto.Description);
                    command.Parameters.AddWithValue("@Image", productDto.Image);
                    command.Parameters.AddWithValue("@Price", productDto.Price);
                    command.Parameters.AddWithValue("@Barcode", productDto.Barcode);

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

        public async Task<List<ProductDto>> SearchProducts(string search)
        {
            var output = new List<ProductDto>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sqlProcedure = "SearchProducts";
                    SqlCommand command = new SqlCommand(sqlProcedure, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Search", search);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync().ConfigureAwait(false))
                        {
                            var product = new ProductDto();

                            product = MapToProductDto(reader);

                            output.Add(product);
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

        private ProductDto MapToProductDto(SqlDataReader reader)
        {
            var product = new ProductDto();

            product.Id = Convert.ToInt32(reader["Id"]);
            product.CategoryId = Convert.ToInt32(reader["CategoryId"]);
            product.CategoryDescription = reader["CategoryDescription"].ToString();
            product.UserName = reader["Username"].ToString();
            product.Price = Convert.ToDecimal(reader["Price"]);
            product.Image = reader["Image"].ToString();
            product.Name = reader["Name"].ToString();
            product.Description = reader["Description"].ToString();
            product.Barcode = reader["Barcode"].ToString();
            product.Created = Convert.ToDateTime(reader["Created"]);

            return product;
        }

        
    }
}
