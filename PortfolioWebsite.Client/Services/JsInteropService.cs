using Microsoft.JSInterop;
using PortfolioWebsite.Client.Services.Contracts;

namespace PortfolioWebsite.Client.Services
{
    public class JsInteropService : IJsInteropService
    {
        private readonly IJSRuntime _jsRuntime;

        public JsInteropService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task ToggleHamburger()
        {
            await _jsRuntime.InvokeVoidAsync("toggleHamburger");
        }
    }
}
