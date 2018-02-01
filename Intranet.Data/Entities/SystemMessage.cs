using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Intranet.Data.Enumerators;

namespace Intranet.Data.Entities
{
    public class SystemMessage
    {
        public SystemMessage()
        {
            Message = string.Empty;
            RedirectUrl = string.Empty;
        }

        public SystemMessageType SystemMessageType { get; set; }
        public string Message { get; set; }
        public string RedirectUrl { get; set; }
    }
}
