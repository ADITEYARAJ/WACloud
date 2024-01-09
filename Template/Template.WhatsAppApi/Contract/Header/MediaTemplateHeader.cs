using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Template.WhatsAppApi.Contract.Header
{
    /// <summary>
    /// Media Header can be an image, video or document as PDF format. All media must be uploaded with the Resumable Upload API.
    /// </summary>
    public class MediaTemplateHeader : TemplateHeader
    {
        [JsonProperty("example")]
        public HeaderHandle Example { get; set; }
    }

    public class HeaderHandle
    {
        /// <summary>
        /// Uploaded file's file handle value from Resumable Upload API response.
        /// </summary>
        [JsonProperty("header_handle")]
        public IEnumerable<string> _media { get; set; }
    }
}
