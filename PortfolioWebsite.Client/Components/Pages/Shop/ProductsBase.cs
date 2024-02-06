using Microsoft.AspNetCore.Components;
using PortfolioWebsite.Client.Services.Contracts;
using PortfolioWebsite.Models.DTOs;

namespace PortfolioWebsite.Client.Components.Pages.Shop
{
	public class ProductsBase : ComponentBase
	{
		[Inject]
		public IProductService ProductService { get; set; }

		[Inject]
		public IShoppingCartService ShoppingCartService { get; set; }

		[Inject]
		public IManageCartItemsLocalStorageService ManageCartItemsLocalStorageService { get; set; }

		[Inject]
		public IManageProductsLocalStorageService ManageProductsLocalStorageService { get; set; }


		public IEnumerable<ProductDto> Products;

		private bool firstRenderCompleted = false;







		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (firstRender && !firstRenderCompleted)
			{
				firstRenderCompleted = true;

				await ClearLocalStorage();
				Products = await ManageProductsLocalStorageService.GetCollection();

				var shoppingCartItems = await ManageCartItemsLocalStorageService.GetCollection();

				var totalQty = shoppingCartItems.Sum(i => i.Qty);

				ShoppingCartService.RaiseEventOnShoppingCartChanged(totalQty);

				StateHasChanged();
			}
		}



		protected IOrderedEnumerable<IGrouping<int, ProductDto>> GetFroupedProductsByCategory()
		{
			return from product in Products
				   group product by product.CategoryId into prodByCatGroup
				   orderby prodByCatGroup.Key
				   select prodByCatGroup;
		}

		protected string GetCategoryName(IGrouping<int, ProductDto> groupedProductDtos)
		{
			return groupedProductDtos.FirstOrDefault(pg => pg.CategoryId == groupedProductDtos.Key).CategoryName;
		}

		private async Task ClearLocalStorage()
		{
			await ManageCartItemsLocalStorageService.RemoveCollection();
			await ManageProductsLocalStorageService.RemoveCollection();
		}
	}
}
