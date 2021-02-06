using System.Threading.Tasks;

using Blazored.LocalStorage;

using Essiq.Showroom.Client.Utils;

namespace Essiq.Showroom.Client
{
    public sealed class AppContext
    {
        private readonly ILocalStorageService localStorage;

        public AppContext(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
        }

        public async Task<string> GetAuthTokenAsync()
        {
            return await localStorage.GetItemAsync<string>(Constants.AuthTokenKey);
        }

        public async Task SetAuthTokenAsync(string token)
        {
            await localStorage.SetItemAsync(Constants.AuthTokenKey, token);
        }
    }
}
