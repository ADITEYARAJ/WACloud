using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Template.WhatsAppApi.Contract.Buttons
{
    public class CopyCodeButtonsTemplate
    {
        /// <summary>
        /// Type of the Copy code button is "COPY_CODE".
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Maximum 15 characters, string to 
        /// </summary>
        [JsonProperty("example")]
        public string Example { get; set; }
    }
}
