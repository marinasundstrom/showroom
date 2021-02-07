using System.Threading.Tasks;

using Microsoft.JSInterop;

namespace Showroom.Client.Services
{
    public sealed class JSHelpers : IJSHelpers
    {
        private readonly IJSRuntime jsRuntime;

        public JSHelpers(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public async Task Alert(string message)
        {
            await jsRuntime.InvokeVoidAsync("helpers.alert", message);
        }

        public async Task<bool> Confirm(string message)
        {
            return await jsRuntime.InvokeAsync<bool>("helpers.confirm", message);
        }

        public async Task<string> Prompt(string message, string @default)
        {
            return await jsRuntime.InvokeAsync<string>("helpers.prompt", message, @default);
        }

        public async Task SetTitle(string title)
        {
            await jsRuntime.InvokeVoidAsync("helpers.setTitle", title);
        }

        public async Task ScrollToTop()
        {
            await jsRuntime.InvokeVoidAsync("helpers.scrollToTop");
        }

        public async Task ScrollToBottom()
        {
            await jsRuntime.InvokeVoidAsync("helpers.scrollToBottom");
        }
    }
}
