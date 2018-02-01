using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Intranet.Controllers
{
    public class BaseController : Controller
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            string rootApplication = System.Configuration.ConfigurationManager.AppSettings["rootApplication"].ToString();

            if (Helpers.Sessions.ADUser == null)
            {
                requestContext.HttpContext.Response.Redirect(rootApplication);
            }

            ViewData["rootApplication"] = rootApplication;

            new Data.ADO.HistoryLoginADO().LogModule(Helpers.Sessions.ADUser, requestContext.HttpContext.Request.Url.ToString());

            base.Initialize(requestContext);
        }

        internal void VerificaPermissao(string controladorNome)
        {
            string rootApplication = System.Configuration.ConfigurationManager.AppSettings["rootApplication"].ToString();

            var menus = (from n1 in Helpers.Sessions.ADUser.Menus
                         where n1.ControladorNome == controladorNome
                         || n1.Menus.Count(n2 => n2.ControladorNome == controladorNome) > 0
                         select n1.Id).Count();

            if (menus.Equals(0))
            {
                Helpers.Sessions.MensagemLogin = Resources.Geral.AcessoNegado;
                this.HttpContext.Response.Redirect(rootApplication);
            }
        }
    }
}
