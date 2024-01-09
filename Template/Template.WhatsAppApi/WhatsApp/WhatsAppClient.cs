namespace Template.WhatsAppApi.WhatsApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Core.Common.Http;
    using Template.WhatsAppApi.Contract;

    public class WhatsAppClient : IWhatsAppClient
    {
        /// <summary>
        /// TODO: Set this version value in RCS
        /// </summary>
        public const string waApiVersion = "v17.0";

        /// <summary>
        /// public static AuthenticationHeaderValue AuthToken;
        /// </summary>

        public WhatsAppClient() 
        {
        }

        public async Task<HttpResponseMessage> GetAllTemplate(string businessId)
        {
            var getUri = UriHelper.BuildWithPath(this.GetWhatsAppUri(), Routes.GetTemplates, businessId);
            AuthenticationHeaderValue AuthToken = null;

            var resultResponse = await HttpHelper.GetHttpResponseAsync(getUri?.Uri, CancellationToken.None, authorizationHeader: AuthToken);
            return resultResponse;
        }

        public HttpResponseMessage GetTemplateByName(string businessId, string template_name)
        {
            throw new NotImplementedException();
        }

        public HttpResponseMessage GetTemplateByTemplateId(string templateId)
        {
            throw new NotImplementedException();
        }

        public HttpResponseMessage GetTemplateNameSpace(string businessId)
        {
            throw new NotImplementedException();
        }

        public async Task<TemplateStatusResponse> PostTemplate(HttpContent postContent, string businessId, AuthenticationHeaderValue AuthToken = null)
        {
            var postUri = UriHelper.BuildWithPath(this.GetWhatsAppUri(), Routes.PostTemplates, businessId);
            var authenticationToken = AuthToken;

            var resultResponse = await HttpHelper.PostWithResultAsync<TemplateStatusResponse>(postUri.Uri, postContent, authenticationToken);
            return resultResponse;
        }

        public async Task<HttpResponseMessage> GetAllTemplateTest()
        {
            HttpResponseMessage resultResponse = null;
            AuthenticationHeaderValue AuthToken = null;
            try
            {
                resultResponse = await HttpHelper.GetHttpResponseAsync(new Uri("https://graph.facebook.com/v17.0/144293665436297/message_templates"), CancellationToken.None, authorizationHeader: AuthToken);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return resultResponse;

        }

        ////private async Task<AuthenticationHeaderValue> GetAuthenticationHeaderValue()
        ////{
        ////    ////return AuthToken;
        ////}

        private Uri GetWhatsAppUri()
        {
            return new Uri($" https://graph.facebook.com/v17.0/");
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
