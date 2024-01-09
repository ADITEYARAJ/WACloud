using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Http
{
    public static class HttpStatusCodeHelper
    {
        public static bool IsSuccessCode(this HttpStatusCode httpStatusCode)
        {
            return (int)httpStatusCode >= 200 && (int)httpStatusCode < 300;
        }

        public static bool IsErrorCode(this HttpStatusCode httpStatusCode)
        {
            return (int)httpStatusCode >= 400 && (int)httpStatusCode < 600;
        }

        public static bool IsServerErrorCode(this HttpStatusCode httpStatusCode)
        {
            return (int)httpStatusCode >= 500 && (int)httpStatusCode < 600;
        }
    }
}
