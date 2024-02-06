using Microsoft.AspNetCore.Components;
using PortfolioWebsite.Models.DTOs;

namespace PortfolioWebsite.Client.Components.Pages.Shop
{
	public class DisplayProductsBase : ComponentBase
	{
		[Parameter]
		public IEnumerable<ProductDto> Products { get; set; }
	}
}
