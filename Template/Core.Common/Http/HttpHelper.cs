namespace Core.Common.Http
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Reflection.Metadata;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    public static class HttpHelper
    {

        /// <summary>
        /// TO DO - Keeping these value constant for now, will make these configurable.
        /// </summary>
        private const int PoolConnectionLifetimeInMinutes = 10;

        private const int PoolConnectionIdleTimeoutInMinutes = 5;


        /// <summary>
        /// Making this property private to not accessible directly.
        /// </summary>
        private static HttpClient sharedHttpClient;

        /// <summary>
        /// Get the  HttpClient
        /// </summary>
        public static HttpClient HttpClient
        {
            get
            {
                HttpClient httpClient = sharedHttpClient ?? (sharedHttpClient = new HttpClient(SocketsHttpHandler()));
                
                return httpClient; 
            }
        }

        /// <summary>
        /// Gets the shared JsonMediaTypeFormatter
        /// </summary>
        private static JsonMediaTypeFormatter JsonFormatter { get; } = new JsonMediaTypeFormatter();


        /// <summary>
        /// Gets a Http Response message from a REST service endpoint, specified in the requestUri
        /// </summary>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <param name="authorizationHeader">The optional authorization header.</param>
        /// <param name="optionalHeaderKeyValues">The optional header key values.</param>
        /// <returns>Http response message</returns>
        public static async Task<HttpResponseMessage> GetHttpResponseAsync(Uri requestUri, CancellationToken cancellationToken, AuthenticationHeaderValue authorizationHeader = null, IDictionary<string, string> optionalHeaderKeyValues = null)
        {
            try
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri))
                {
                    requestMessage.Headers.Authorization = authorizationHeader;
                    AddHeaders(requestMessage.Headers, optionalHeaderKeyValues);

                    return await HttpClient.SendAsync(requestMessage, cancellationToken).ConfigureAwait(false);
                }
            }
            catch (TaskCanceledException)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// Get the Http response in Result format of <type name ="IResult" />
        /// </summary>
        /// <typeparam name="IResult"></typeparam>
        /// <param name="requestUri"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="authenticationHeader"></param>
        /// <param name="optionalHeaderKeyValues"></param>
        /// <param name="ensureSucessStatusCode"></param>
        /// <param name="treatNotFoundAsError"></param>
        /// <param name="messageHandler"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task<TResult> GetHttpResponseEntityAsync<TResult>(Uri requestUri,
            CancellationToken cancellationToken,
            AuthenticationHeaderValue authenticationHeader,
            IDictionary<string, string> optionalHeaderKeyValues = null,
            bool ensureSucessStatusCode = false,
            bool treatNotFoundAsError = true,
            HttpMessageHandler messageHandler = null)
        {
            if (requestUri == null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri))
            {
                requestMessage.Headers.Authorization = authenticationHeader;
                AddHeaders(requestMessage.Headers, optionalHeaderKeyValues);
                
                var httpClient = messageHandler != null ? new HttpClient(messageHandler) : HttpClient;

                var httpResponse = await httpClient.SendAsync(requestMessage).ConfigureAwait(false);
                return await ProcessResponseAsync<TResult>(httpResponse, ensureSucessStatusCode, treatNotFoundAsError);
            }
        }


        /// <summary>
        /// Get the Http response in Result format of <type name ="IResult" /> with cookies 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="requestUri"></param>
        /// <param name="cookie"></param>
        /// <param name="ensureSuccessStatusCode"></param>
        /// <param name="treatNotFoundAsError"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task<TResult> GetResponseWithCookieAsync<TResult>(Uri requestUri, Cookie cookie, bool ensureSuccessStatusCode = false, bool treatNotFoundAsError = true)
        {
            if (requestUri == null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            var httpClientHandler = SocketsHttpHandler();

            if (cookie != null)
            {
                httpClientHandler.UseCookies = true;
                httpClientHandler.CookieContainer = new CookieContainer();
                httpClientHandler.CookieContainer.Add(cookie);
            }

            using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri))
            {
                var httpClient = new HttpClient(httpClientHandler);
                var httpResponse = await httpClient.SendAsync(requestMessage).ConfigureAwait(false);
                return await ProcessResponseAsync<TResult>(httpResponse, ensureSuccessStatusCode, treatNotFoundAsError).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Put the specific content of <Type name = "T"/> to a request endpoint. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="content"></param>
        /// <param name="authenticationHeader"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task PutRequestAsync<T>(Uri uri, T content, AuthenticationHeaderValue authenticationHeader = null) where T : class
        {
            if (uri == null)
            {
                throw new ArgumentNullException($"{nameof(uri)} can not be null.");
            }

            if (content == null)
            {
                throw new ArgumentNullException("content can not be null." );
            }

            using (var requestMessage =  new HttpRequestMessage(HttpMethod.Put, uri))
            {
                requestMessage.Headers.Authorization = authenticationHeader;
                requestMessage.Content = new ObjectContent<T>(content, JsonFormatter);

                await HttpClient.SendAsync(requestMessage).ConfigureAwait(false);
            }
        }


        /// <summary>
        /// Gets an entity or type <typeparamref name="TResult" /> from a REST service endpoint, specified in the requestUri.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="uri"></param>
        /// <param name="content"></param>
        /// <param name="authenticationHeaderValue"></param>
        /// <param name="ensureSucessCode"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task<TResult> PutWithResultAsync<TResult>(Uri uri, HttpContent content, AuthenticationHeaderValue authenticationHeaderValue, bool ensureSucessCode)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("Uri can not be null.");
            }

            if (content == null)
            {
                throw new ArgumentNullException("content can not be null.");
            }


            using (var requestMessage = new HttpRequestMessage(HttpMethod.Put, uri))
            {
                requestMessage.Content = content;
                requestMessage.Headers.Authorization = authenticationHeaderValue;

                var httpResponse = await HttpClient.SendAsync(requestMessage).ConfigureAwait(false);

                return await ProcessResponseAsync<TResult>(httpResponse, ensureSucessCode).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Put the specific content of <Type name = "T"/> to a request endpoint. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="content"></param>
        /// <param name="authenticationHeader"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task PostRequestAsync<T>(Uri uri, T content, AuthenticationHeaderValue authenticationHeader = null) where T : class
        {
            if (uri == null)
            {
                throw new ArgumentNullException($"{nameof(uri)} can not be null.");
            }

            if (content == null)
            {
                throw new ArgumentNullException("content can not be null.");
            }

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri))
            {
                requestMessage.Headers.Authorization = authenticationHeader;
                requestMessage.Content = new ObjectContent<T>(content, JsonFormatter);

                await HttpClient.SendAsync(requestMessage).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Gets an entity or type <typeparamref name="TResult" /> from a REST service endpoint, specified in the requestUri.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="uri"></param>
        /// <param name="content"></param>
        /// <param name="authenticationHeaderValue"></param>
        /// <param name="ensureSucessCode"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task<TResult> PostWithResultAsync<TResult>(Uri uri, HttpContent content, AuthenticationHeaderValue authenticationHeaderValue, IDictionary<string, string> optionalHeaderKeyValues = null,  bool ensureSucessCode = false)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("Uri can not be null.");
            }

            if (content == null)
            {
                throw new ArgumentNullException("content can not be null.");
            }


            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri))
            {
                requestMessage.Content = content;
                requestMessage.Headers.Authorization = authenticationHeaderValue;

                var httpResponse = await HttpClient.SendAsync(requestMessage).ConfigureAwait(false);

                return await ProcessResponseAsync<TResult>(httpResponse, ensureSucessCode).ConfigureAwait(false);
            }
        }

        public static async Task DeleteAsync(Uri uri, AuthenticationHeaderValue authenticationHeaderValue, bool ensureSucessStatusCode = false)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("Uri can not be null");
            }

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Delete, uri))
            {
                requestMessage.Headers.Authorization = authenticationHeaderValue;

                var httpResponse = await HttpClient.SendAsync(requestMessage).ConfigureAwait(false);

                ProcessResponse(httpResponse, ensureSucessStatusCode);
            }
        }

        public static async Task<TResult> ProcessResponseAsync<TResult>(HttpResponseMessage httpResponse, bool ensureSuccessStatusCode = false, bool treatNotFoundAsError = true)
        {
            if (httpResponse == null)
            {
                throw new ArgumentNullException(nameof(httpResponse));
            }

            if (httpResponse.IsSuccessStatusCode)
            {
                return await httpResponse.Content.ReadAsAsync<TResult>().ConfigureAwait(false);
            }

            TraceFailure(httpResponse);

            if (ensureSuccessStatusCode)
            {
                if (treatNotFoundAsError || httpResponse.StatusCode != HttpStatusCode.NotFound)
                {
                    httpResponse.EnsureSuccessStatusCode();
                }
            }

            return await Completed(default(TResult)).ConfigureAwait(false);
        }

        private static void ProcessResponse(HttpResponseMessage httpResponse, bool ensureSuccessStatusCode = false)
        {
            if (httpResponse == null)
            {
                throw new ArgumentNullException(nameof(httpResponse));
            }

            if (httpResponse.IsSuccessStatusCode)
            {
                return;
            }

            TraceFailure(httpResponse);

            if (ensureSuccessStatusCode)
            {
                httpResponse.EnsureSuccessStatusCode();
            }
        }

        /// <summary>
        /// TO Do - Use this for logging the failure response and reason.
        /// </summary>
        /// <param name="httpResponseMessage"></param>
        private static void TraceFailure(HttpResponseMessage httpResponseMessage)
        {
            var endPoint = (httpResponseMessage.RequestMessage != null && httpResponseMessage.RequestMessage.RequestUri != null) ? httpResponseMessage.RequestMessage.RequestUri.AbsoluteUri : string.Empty;
            
        }
        private static void AddHeaders(HttpRequestHeaders existingHeader, IDictionary<string, string> optionalHeaderKeyValues)
        {
            if (optionalHeaderKeyValues == null || optionalHeaderKeyValues.Count == 0)
            {
                return;
            }

            foreach(var keyValuePair in optionalHeaderKeyValues)
            {
                existingHeader.Add(keyValuePair.Key, keyValuePair.Value);
            }
        }


        private static SocketsHttpHandler SocketsHttpHandler()
        {
            var socketHttpHandler = new SocketsHttpHandler();

            socketHttpHandler.PooledConnectionIdleTimeout = TimeSpan.FromMinutes(PoolConnectionIdleTimeoutInMinutes);
            socketHttpHandler.PooledConnectionLifetime = TimeSpan.FromMinutes(PoolConnectionLifetimeInMinutes);

            return socketHttpHandler;
        }

        public static Task<T> Completed<T>(T value)
        {
            var tcs = new TaskCompletionSource<T>();
            tcs.SetResult(value);
            return tcs.Task;
        }
    }
}
