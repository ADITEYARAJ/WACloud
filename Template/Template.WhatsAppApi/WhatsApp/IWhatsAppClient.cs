namespace Template.WhatsAppApi.WhatsApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Template.WhatsAppApi.Contract;

    public interface IWhatsAppClient : IDisposable
    {
        HttpResponseMessage GetTemplateByTemplateId(string templateId);

        HttpResponseMessage GetTemplateByName(string businessId, string template_name);

        Task<HttpResponseMessage> GetAllTemplate(string businessId);

        HttpResponseMessage GetTemplateNameSpace(string businessId);

        Task<TemplateStatusResponse> PostTemplate(HttpContent postContent, string businessId, AuthenticationHeaderValue AuthToken = null);

        Task<HttpResponseMessage> GetAllTemplateTest();
    }
}
