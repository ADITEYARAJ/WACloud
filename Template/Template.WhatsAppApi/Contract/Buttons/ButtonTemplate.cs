namespace Template.WhatsAppApi.Contract.Buttons
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public class ButtonTemplate
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("buttons")]
        public IEnumerable<object> Buttons { get; set; }
    }
}
