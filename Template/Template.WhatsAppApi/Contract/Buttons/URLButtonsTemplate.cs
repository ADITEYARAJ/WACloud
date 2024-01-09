using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Template.WhatsAppApi.Contract.Buttons
{
    /// <summary>
    /// The Templates are limited to two URL buttons.
    /// </summary>
    public class URLButtonsTemplate
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// 25 characters maximum. Button label text, Supports 1 variable.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// 2000 characters maximum. Supports 1 variable, appended to the end of the URL string.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// 2000 characters maximum. URL of website. Supports 1 variable.
        /// </summary>
        [JsonProperty("example")]
        public IEnumerable<string> Example { get; set; }
    }
}
