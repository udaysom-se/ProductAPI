﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ProductAPI.Models
{
	public class Product
	{
		public int Id { get; set; }
		public required string Name { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal Price { get; set; }
	}
}
