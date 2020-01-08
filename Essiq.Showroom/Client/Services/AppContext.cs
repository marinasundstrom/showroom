using System.Threading.Tasks;

using Blazor.Extensions.Storage.Interfaces;

using Essiq.Showroom.Client.Utils;

namespace Essiq.Showroom.Client
{
    public sealed class AppContext
    {
        private readonly ILocalStorage localStorage;

        public AppContext(ILocalStorage localStorage)
        {
            this.localStorage = localStorage;
        }

        public async Task<string> GetAuthTokenAsync()
        {
            return await localStorage.GetItem<string>(Constants.AuthTokenKey);
        }

        public async Task SetAuthTokenAsync(string token)
        {
            await localStorage.SetItem(Constants.AuthTokenKey, token);
        }
    }
}
