using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.WhatsAppApi.Contract
{
    [Serializable]
    public class TemplateStatusResponse
    {
        public string Id { get; set; }

        public string Status { get; set; }

        public string Category { get; set; }
    }
}
