using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices;

namespace Intranet.Helpers
{
    public class Application
    {
        public static List<DirectoryEntry> UsersAD
        {
            get
            {
                return (List<DirectoryEntry>)HttpContext.Current.Application["UsersAD"];
            }
            set
            {
                HttpContext.Current.Application["UsersAD"] = value;
            }
        }
    }
}