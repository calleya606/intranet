using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intranet.ExtensionMethods;

namespace Intranet.Controllers
{
    public class ModalPerfilController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult CarregarTudo(FormCollection form)
        {
            Entities.JsonReturnJS jsonResultado = new Entities.JsonReturnJS();
            jsonResultado.Data = new Data.ADO.BinaryItensADO().CarregarTudo<Data.Entities.Perfil>().OrderBy(n=>n.Nome).ToList();
            return Json(jsonResultado);
        }
    }
}
