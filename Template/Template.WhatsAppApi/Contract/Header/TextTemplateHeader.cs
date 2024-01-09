using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Template.WhatsAppApi.Contract.Header
{
    public class TextTemplateHeader : TemplateHeader
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("example")]
        public HeaderText Example { get; set; }

    }

    public class HeaderText
    {
        [JsonProperty("header_text")]
        public IEnumerable<string> _text { get; set; }
    }
}
