using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.WhatsAppApi
{
    /// <summary>
    /// Template related WhatsApp routes
    /// </summary>
    public class Routes
    {
        /// <summary>
        /// Get the template by template Id
        /// </summary>
        public const string TemplateById = "{id}";

        /// <summary>
        /// Get the template by template name.
        /// </summary>
        public const string TemplateByName = "{wabaId}/message_templates?name={template_name}";

        /// <summary>
        /// Get all the template by WhatsApp Business  Id.
        /// </summary>
        public const string GetTemplates = "{wabaId}/message_templates";

        /// <summary>
        /// Namespace related to template
        /// </summary>
        public const string Namespace = "{wabaId}?fields=message_template_namespace";

        /// <summary>
        /// Post a template in WhatsApp Business Id.
        /// </summary>
        public const string PostTemplates = "{wabaId}/message_templates";

        /// <summary>
        /// Delete template by template Id.
        /// </summary>
        public const string DeleteTemplateById = "{wabaId}/message_templates?hsm_id={id}&name=order_confirmation";

        private static readonly string[] ApiVersions = { "v1" };
        
        public static IEnumerable<string> SupportedApiVersions => ApiVersions;

    }
}
