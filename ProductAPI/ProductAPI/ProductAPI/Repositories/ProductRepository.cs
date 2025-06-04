using Microsoft.Data.SqlClient;
using ProductAPI.Interfaces;
using ProductAPI.Models;

namespace ProductAPI.Repositories
{
	public class ProductRepository(IConfiguration configuration) : IProductRepository
	{
		private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection") ??
													throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

		public async Task AddProduct(Product product)
		{
			using var connection = new SqlConnection(_connectionString);
			var query = "INSERT INTO Products (Name, Price) VALUES (@Name, @Price)";
			
			using var command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@Name", product.Name);
			command.Parameters.AddWithValue("@Price", product.Price);

			await connection.OpenAsync();
			await command.ExecuteNonQueryAsync();
		}

		public async Task DeleteProduct(int id)
		{
			using var connection = new SqlConnection(_connectionString);
			var query = "DELETE FROM Products WHERE Id = @Id";

			using var command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@Id", id);

			await connection.OpenAsync();
			await command.ExecuteNonQueryAsync();
		}

		public async Task<Product?> GetProductById(int id)
		{
			Product? product = null;
			using (var connection = new SqlConnection(_connectionString))
			{
				var query = "SELECT Id, Name, Price FROM Products WHERE Id = @Id";
				using var command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@Id", id);
				await connection.OpenAsync();

				using var reader = await command.ExecuteReaderAsync();
				if (await reader.ReadAsync())
				{
					product = new Product
					{
						Id = reader.GetInt32(0),
						Name = reader.GetString(1),
						Price = reader.GetDecimal(2)
					};
				}
			}
			return product;
		}

		public async Task<IEnumerable<Product>> GetProducts()
		{
			var products = new List<Product>();
			using (var connection = new SqlConnection(_connectionString))
			{
				var query = "SELECT Id, Name, Price FROM Products";
				using var command = new SqlCommand(query, connection);
				await connection.OpenAsync();
				using var reader = await command.ExecuteReaderAsync();
				while (await reader.ReadAsync())
				{
					products.Add(new Product
					{
						Id = reader.GetInt32(0),
						Name = reader.GetString(1),
						Price = reader.GetDecimal(2)
					});
				}
			}
			return products;
		}

		public async Task UpdateProduct(Product product)
		{
			using var connection = new SqlConnection(_connectionString);
			var query = "UPDATE Products SET Name = @Name, Price = @Price WHERE Id = @Id";

			using var command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@Id", product.Id);
			command.Parameters.AddWithValue("@Name", product.Name);
			command.Parameters.AddWithValue("@Price", product.Price);

			await connection.OpenAsync();
			await command.ExecuteNonQueryAsync();
		}
	}
}
