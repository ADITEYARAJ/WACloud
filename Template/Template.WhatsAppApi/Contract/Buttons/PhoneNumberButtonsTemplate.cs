using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Template.WhatsAppApi.Contract.Buttons
{
    public class PhoneNumberButtonsTemplate
    {
        /// <summary>
        /// Type of the property is "PHONE_NUMBER"
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Button Label text.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Alphanumeric string. Maximum of 20 characters.
        /// </summary>
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }

    }
}
