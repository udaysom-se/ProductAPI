using ProductAPI.Models;

namespace ProductAPI.Interfaces
{
	public interface IProductRepository
	{
		Task<IEnumerable<Product>> GetProducts();
		Task<Product?> GetProductById(int id);
		Task AddProduct(Product product);
		Task UpdateProduct(Product product);
		Task DeleteProduct(int id);
	}
}
