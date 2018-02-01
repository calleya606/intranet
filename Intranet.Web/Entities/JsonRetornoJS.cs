using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Entities
{
    public class JsonReturnJS
    {
        public JsonReturnJS()
        {
            ErrorMessage = string.Empty;
            Message = string.Empty;
            Criticas = new List<JsonCriticaJS>();
            Error = false;
        }

        public object Data { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public bool Error { get; set; }
        public List<JsonCriticaJS> Criticas { get; set; }
        public Object Object { get; set; }
    }
}