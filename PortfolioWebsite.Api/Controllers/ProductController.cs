﻿using Microsoft.AspNetCore.Mvc;
using PortfolioWebsite.Api.Extensions;
using PortfolioWebsite.Api.Repositories.Contracts;
using PortfolioWebsite.Models.DTOs;

namespace PortfolioWebsite.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductRepository productRepository;

		public ProductController(IProductRepository productRepository)
		{
			this.productRepository = productRepository;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductDto>>> GetItems()
		{
			try
			{
				var products = await this.productRepository.GetItems();
				var productCategories = await this.productRepository.GetCategories();

				if (products == null || productCategories == null)
				{
					return NotFound();
				}
				else
				{
					var productDtos = products.ConvertToDto(productCategories);
					return Ok(productDtos);
				}
			}
			catch (Exception)
			{

				return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
			}
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<ProductDto>> GetItem(int id)
		{
			try
			{
				var product = await this.productRepository.GetItem(id);


				if (product == null)
				{
					return BadRequest();
				}
				else
				{
					var productCategory = await productRepository.GetCategory(product.CategoryId);
					var productDto = product.ConvertToDto(productCategory);
					return Ok(productDto);
				}
			}
			catch (Exception)
			{

				return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
			}
		}

	}
}
