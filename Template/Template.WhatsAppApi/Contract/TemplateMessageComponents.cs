using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.WhatsAppApi.Contract
{
    public class TemplateMessageComponents
    {
        /// <summary>
        /// Different componentType of template.
        /// </summary>
        public ComponentTypes ContentType { get; set; }

        /// <summary>
        /// Different component formats: Text,  Image, Video, Document, Location.
        /// </summary>
        public ComponentFormat ComponentFormat { get; set; }

        /// <summary>
        /// Maximum 60 character allowed for Header, 1024 character for body, 60 character for Footer,
        /// </summary>
        public string Text { get; set; }

    }
}
