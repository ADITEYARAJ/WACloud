using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Template.WhatsAppApi.Contract.Buttons
{
    public class FlowsButtonsTemplate
    {
        /// <summary>
        /// 25 characters maximum.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("flow_id")]
        public string FlowId { get; set; }

        [JsonProperty("flow_action")]
        public string FlowAction { get; set; }

        [JsonProperty("navigate_screen")]
        public string NavigateScreen { get; set; }
    }
}
