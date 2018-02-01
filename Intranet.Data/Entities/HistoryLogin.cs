using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intranet.Data.Entities
{
    public class HistoryLogin
    {
        public string UserAgent { get; set; }
        public string UserName { get; set; }
        public DateTime AccessDate { get; set; }
        public string Ip { get; set; }
        public Int32 TotalAccess { get; set; }
    }
}
