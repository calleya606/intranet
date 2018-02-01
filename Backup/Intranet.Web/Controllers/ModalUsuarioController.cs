using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intranet.ExtensionMethods;

namespace Intranet.Controllers
{
    public class ModalUsuarioController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult CarregarTudo(FormCollection form)
        {
            Entities.JsonReturnJS jsonResultado = new Entities.JsonReturnJS();
            jsonResultado.Data = new Data.ADO.UserADO().CarregarTudo().OrderBy(n=>n.Name).ToList();
            return Json(jsonResultado);
        }
    }
}
