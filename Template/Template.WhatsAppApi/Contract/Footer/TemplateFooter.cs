using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Template.WhatsAppApi.Contract.Footer
{
    public class TemplateFooter
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// 60 character at maximum.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
