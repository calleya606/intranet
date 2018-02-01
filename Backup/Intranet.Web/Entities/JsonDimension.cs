using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Entities
{
    public class JsonDimension
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CreatedByUserName { get; set; }
        public string DataInsert { get; set; }
        public string UpdatedByUserName { get; set; }
        public string DateUpdate { get; set; }
        public string Copy { get; set; }
    }
}