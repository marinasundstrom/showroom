using System;
using System.Threading.Tasks;

using Essiq.Showroom.Client.Utils;
using Essiq.Showroom.Server.Client;

namespace Essiq.Showroom.Client.Services
{
    public sealed class IdentityService : IIdentityService
    {
        private readonly ITokenClient tokenClient;
        private readonly TokenAuthenticationStateProvider tokenAuthenticationStateProvider;
        private readonly IUserClient userClient;
        private UserProfile userProfile;
        private DateTime userProfileLastFetch;

        public IdentityService(ITokenClient tokenClient, IUserClient userClient, TokenAuthenticationStateProvider tokenAuthenticationStateProvider)
        {
            this.tokenClient = tokenClient;
            this.userClient = userClient;
            this.tokenAuthenticationStateProvider = tokenAuthenticationStateProvider;
        }

        public async Task LoginAsync(string usernId, string password)
        {
            var response = await tokenClient.AuthenticateAsync(usernId, password);

            await tokenAuthenticationStateProvider.SetTokenAsync(response.Token);

            await FetchUserProfileAsync();
        }

        public async Task LogoutAsync()
        {
            userProfileLastFetch = DateTime.Now;
            await tokenAuthenticationStateProvider.ClearTokenAsync();
        }

        public async Task<UserProfile> GetUserProfileAsync()
        {
            if (DateTime.Now > userProfileLastFetch.AddMinutes(5))
            {
                await FetchUserProfileAsync();
            }
            return userProfile;
        }

        private async Task FetchUserProfileAsync()
        {
            userProfile = await userClient.GetUserProfileAsync(null);
            userProfileLastFetch = DateTime.Now;
        }
    }
}
