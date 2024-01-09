namespace Template.WhatsAppApi.Contract.Body
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    /// <summary>
    /// Template Body are text only component.
    /// </summary>
    public class TemplateBody
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// 1024 characters is the maximum allowed text.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("example")]
        public BodyText Example { get; set; }

    }

    public class BodyText
    {
        [JsonProperty("body_text")]
        public IEnumerable<string> Body_Texts { get; set; }
    }
}
