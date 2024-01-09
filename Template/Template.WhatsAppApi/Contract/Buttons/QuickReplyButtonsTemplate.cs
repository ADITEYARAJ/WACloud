using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.WhatsAppApi.Contract.Buttons
{
    public class QuickReplyButtonsTemplate
    {

        /// <summary>
        /// Type for this button template is "QUICK_REPLY".
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Button label text.
        /// 
        /// 25 characters maximum.
        /// </summary>
        public string Text { get; set; }

    }
}
