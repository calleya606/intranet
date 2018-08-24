using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intranet.ExtensionMethods;
using System.Text;

namespace Intranet.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            var historyLogin = new Data.ADO.HistoryLoginADO().GetLastHistoryLogin(new Data.Entities.HistoryLogin() { UserName = Helpers.Sessions.ADUser.UserName });
            ViewBag.HistoryLoginMessage = historyLogin;



            return View();
        }
    }
}
