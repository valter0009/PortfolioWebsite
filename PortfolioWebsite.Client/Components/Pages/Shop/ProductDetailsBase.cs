using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Options;
using PortfolioWebsite.Client.Services.Contracts;
using PortfolioWebsite.Models.DTOs;

namespace PortfolioWebsite.Client.Components.Pages.Shop
{
	public class ProductDetailsBase : ComponentBase
	{
		[Parameter]
		public int Id { get; set; }

		[Inject]
		public IProductService ProductService { get; set; }
		[Inject]
		public IShoppingCartService ShoppingCartService { get; set; }
		[Inject]
		public NavigationManager NavigationManager { get; set; }

		[Inject]
		public IManageCartItemsLocalStorageService ManageCartItemsLocalStorageService { get; set; }

		[Inject]
		public IManageProductsLocalStorageService ManageProductsLocalStorageService { get; set; }

		[Inject]
		public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

		[Inject] IOptionsSnapshot<RemoteAuthenticationOptions<ApiAuthorizationProviderOptions>> OptionsSnapshot { get; set; }
		public ProductDto Product { get; set; }

		public string ErrorMessage { get; set; }

		private List<CartItemDto> ShoppingCartItems { get; set; }


		private bool firstRenderCompleted = false;

		protected override async Task OnInitializedAsync()
		{

		}

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (firstRender)
			{
				try
				{
					// Always attempt to fetch product details regardless of authentication status
					Product = await ProductService.GetItem(Id);

					var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
					if (authState.User.Identity.IsAuthenticated)
					{
						ShoppingCartItems = (List<CartItemDto>)await ManageCartItemsLocalStorageService.GetCollection();
					}
				}
				catch (Exception ex)
				{
					ErrorMessage = $"Error loading product details: {ex.Message}";
				}

				StateHasChanged();
			}
		}

		protected async Task AddToCart_Click(CartItemToAddDto cartItemToAddDto)
		{
			try
			{
				var cartItemDto = await ShoppingCartService.AddItem(cartItemToAddDto);
				if (cartItemDto != null)
				{
					ShoppingCartItems.Add(cartItemDto);
					await ManageCartItemsLocalStorageService.SaveCollection(ShoppingCartItems);

				}

				NavigationManager.NavigateTo("/ShoppingCart");
			}
			catch (Exception)
			{

				throw;
			}
		}

		private async Task<ProductDto> GetProductById(int id)
		{
			var productsDtos = await ManageProductsLocalStorageService.GetCollection();
			if (productsDtos != null)
			{
				return productsDtos.SingleOrDefault(x => x.Id == id);
			}
			return null;
		}


		protected void RedirectToLoginAndReturn()
		{
			NavigationManager.NavigateToLogin(OptionsSnapshot.Get(Options.DefaultName).AuthenticationPaths.LogInPath);
		}
	}
}
