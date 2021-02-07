using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Showroom.Server.Client
{
    public abstract class ClientBase
    {
        public Func<Task<string>> RetrieveAuthorizationToken { get; set; }

        // Called by implementing swagger client classes
        protected async Task<HttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken cancellationToken)
        {
            HttpRequestMessage msg = new HttpRequestMessage();

            if (RetrieveAuthorizationToken != null)
            {
                string token = await RetrieveAuthorizationToken();
                msg.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            return msg;
        }

    }
}
