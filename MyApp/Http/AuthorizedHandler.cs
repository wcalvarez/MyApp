using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp
{
    public class AuthorizedHandler : HttpClientHandler
    {
        private readonly Func<Task<string>> _getToken;

        public AuthorizedHandler(Func<Task<string>> getToken)
        {
            _getToken = getToken ?? throw new ArgumentNullException(nameof(getToken));
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _getToken().ConfigureAwait(false);
            //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.Add("api_key", token);
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
