using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp
{
   public class LoggerHttpMessageHandler : DelegatingHandler
    {

        public LoggerHttpMessageHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }


        #region Protected Methods

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var st = new Stopwatch();
            try
            {
                await LogRequest(request).ConfigureAwait(false); ;
                st.Start();
                var res = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
                st.Stop();
                await LogResponse(res, st.Elapsed).ConfigureAwait(false);
                return res;
            }
            catch (Exception ex)
            {
                st.Stop();
                await LogEx(request, ex, st.Elapsed)
                    .ConfigureAwait(false);
                throw;
            }
        }
        #endregion Protected Methods

        #region Private Methods

        private async Task LogEx(HttpRequestMessage request, Exception ex, TimeSpan elapsed)
        {
            var sb = new StringBuilder();

            sb.AppendLine("\n--------------------");

            await AppendRequestLog(sb, request)
                .ConfigureAwait(false);

            sb.AppendLine("--------------------");

            AppendElapsedTimeLog(sb, elapsed);

            sb.AppendLine("--------------------");

            AppendExceptionLog(ex, sb);

            sb.AppendLine("--------------------\n");

            sb.AppendLine("--------------------\n");
            Debug.WriteLine(sb.ToString());
        }

        private async Task LogRequest(HttpRequestMessage request)
        {
            var sb = new StringBuilder();

            sb.AppendLine("\n--------------------");

            await AppendRequestLog(sb, request)
                .ConfigureAwait(false);

            sb.AppendLine("--------------------\n");

            Debug.WriteLine(sb.ToString());
        }

        private async Task LogResponse(HttpResponseMessage response, TimeSpan elapsed)
        {
            var sb = new StringBuilder();

            sb.AppendLine("\n--------------------");

            AppendElapsedTimeLog(sb, elapsed);

            sb.AppendLine("--------------------");

            await AppendResponseLog(sb, response)
                .ConfigureAwait(false);

            sb.AppendLine("--------------------\n");

            Debug.WriteLine(sb.ToString());
        }

        private static async Task AppendResponseLog(StringBuilder sb, HttpResponseMessage response)
        {
            sb.AppendLine(response.ToString());
            if (response.Content != null)
            {
                try
                {
                    var canLogContent = response.Content.Headers.TryGetValues("Content-Type", out IEnumerable<string> values)
                        && values.Any(c =>
                            c.StartsWith("application/json", StringComparison.Ordinal)
                            || c.StartsWith("application/vnd.geo+json", StringComparison.Ordinal)
                            || c.StartsWith("application/x-www-form-urlencoded", StringComparison.Ordinal));

                    var body = "";
                    if (canLogContent)
                    {
                        body = await response.Content.ReadAsStringAsync()
                            .ConfigureAwait(false);
                    }
                    else
                    {
                        body = "Body not allowed to be parsed";
                    }

                    sb.AppendLine($"BODY:\n{body}");
                }
                catch (Exception)
                {
                    sb.AppendLine("Exception during reading the BODY");
                }
            }
        }

        private static void AppendExceptionLog(Exception ex, StringBuilder sb)
        {
            sb.Append("Exception:\n");
            sb.AppendLine(ex.ToString());
        }

        private static void AppendElapsedTimeLog(StringBuilder sb, TimeSpan elapsed)
        {
            sb.AppendLine($"Respond in: {elapsed.Milliseconds}ms");
        }

        private static async Task AppendRequestLog(StringBuilder sb, HttpRequestMessage request)
        {
            sb.AppendLine(request.ToString());
            try
            {
                if (request.Content?.Headers.ContentType == null)
                {
                    sb.AppendLine("BODY: (null)");
                    return;
                }

                var body = await request.Content.ReadAsStringAsync()
                    .ConfigureAwait(false);
                sb.AppendLine(body.Length > 10000
                    ? "BODY => Body Too Large!!!"
                    : $"BODY:\n{body}");
            }
            catch (Exception)
            {
                sb.AppendLine("Exception during reading the BODY");
            }
        }
        #endregion Private Methods
    }
}
