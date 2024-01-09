using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Template.WhatsAppApi.Contract
{
    [Serializable]
    public class TemplateRequests
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("category")]
        public object Category { get; set; }

        ////public bool AllowCategoryChange { get; set; }

        /// <summary>
        /// TODO: Change to enum.
        /// </summary>
        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("components")]
        public IEnumerable<JObject> TemplateMessageComponents { get; set; } = new List<JObject>();
    }
}
