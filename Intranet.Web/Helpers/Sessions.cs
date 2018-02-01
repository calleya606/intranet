using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices;
using Intranet.Data.Entities;

namespace Intranet.Helpers
{
    public class Sessions
    {
        public static Data.Entities.User ADUser
        {
            get
            {
                return (Data.Entities.User)HttpContext.Current.Session["ADUser"];
            }
            set
            {
                HttpContext.Current.Session["ADUser"] = value;
            }
        }

        public static List<DirectoryEntry> UsersAD
        {
            get
            {
                return (List<DirectoryEntry>)HttpContext.Current.Session["UsersAD"];
            }
            set
            {
                HttpContext.Current.Session["UsersAD"] = value;
            }
        }

        public static string MensagemLogin
        {
            get
            {
                return HttpContext.Current.Session["MensagemLogin"] as string;
            }
            set
            {
                HttpContext.Current.Session["MensagemLogin"] = value;
            }
        }
    }
}