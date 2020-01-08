using System.Security.Claims;
using System.Threading.Tasks;

using Blazor.Extensions.Storage.Interfaces;

using Microsoft.AspNetCore.Components.Authorization;

namespace Essiq.Showroom.Client.Utils
{
    public class TokenAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorage _localStorage;

        public TokenAuthenticationStateProvider(ILocalStorage localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<string> GetTokenAsync()
        {
            return await _localStorage.GetItem<string>(Constants.AuthTokenKey);
        }

        public async Task SetTokenAsync(string token)
        {
            await _localStorage.SetItem(Constants.AuthTokenKey, token);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string token = await GetTokenAsync();
            ClaimsIdentity identity = string.IsNullOrEmpty(token)
                ? new ClaimsIdentity()
                : new ClaimsIdentity(ServiceExtensions.ParseClaimsFromJwt(token), "jwt");
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }
    }

    class Constants
    {
        public const string AuthTokenKey = "authToken";
    }
}
