using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Entities
{
    public class JsonCriticaJS
    {
        public JsonCriticaJS()
        {
            FieldId = string.Empty;
            AbaId = string.Empty;
            Message = string.Empty;
        }

        public string FieldId { get; set; }
        public string Message { get; set; }
        public string AbaId { get; set; }
    }
}