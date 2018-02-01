using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Helpers
{
    public class AppSettings
    {
        public static string RootApplication
        {
            get
            {
                var temp = string.Empty;
                if (System.Configuration.ConfigurationManager.AppSettings["rootApplication"] != null)
                {
                    temp = System.Configuration.ConfigurationManager.AppSettings["rootApplication"];
                }

                return temp;
            }            
        }

        public static string AppTitle
        {
            get
            {
                var temp = string.Empty;
                if (System.Configuration.ConfigurationManager.AppSettings["appTitle"] != null)
                {
                    temp = System.Configuration.ConfigurationManager.AppSettings["appTitle"];
                }

                return temp;
            }
        }
    }
}