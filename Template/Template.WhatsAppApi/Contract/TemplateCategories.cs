using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.WhatsAppApi.Contract
{
    public enum TemplateCategories
    {
        /// <summary>
        /// Category for awareness/sales
        /// </summary>
        Marketing = 0,

        /// <summary>
        /// Category initiated in response for user actions, eg : transaction,account, subscription
        /// </summary>
        Utility = 1,

        /// <summary>
        /// Category to enable BU to authenticate users with OTPs, verification, recovery.
        /// </summary>
        Authentication = 2
    }
}
