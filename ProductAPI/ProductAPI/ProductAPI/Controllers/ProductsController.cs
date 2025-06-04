using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Interfaces;
using ProductAPI.Models;

namespace ProductAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController(IProductRepository productRepository) : ControllerBase
	{
		private readonly IProductRepository _productRepository = productRepository;

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
		{
			return Ok(await _productRepository.GetProducts());
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			var product = await _productRepository.GetProductById(id);
			return product == null ? NotFound() : Ok(product);
		}

		[HttpPost]
		public async Task<ActionResult> AddProduct(Product product)
		{
			await _productRepository.AddProduct(product);
			return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateProduct(int id, Product product)
		{
			product.Id = id;
			await _productRepository.UpdateProduct(product);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteProduct(int id)
		{
			await _productRepository.DeleteProduct(id);
			return NoContent();
		}
	}
}
