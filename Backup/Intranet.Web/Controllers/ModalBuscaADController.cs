using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intranet.ExtensionMethods;

namespace Intranet.Controllers
{
    public class ModalBuscaADController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult BuscaUsuarioAD(FormCollection form)
        {
            Entities.JsonReturnJS jsonResultado = new Entities.JsonReturnJS();

            Data.Entities.User usuario = new Data.Entities.User();
            usuario.UserName = form["txtUserName"].ToString();
            usuario = new Data.ADO.UserADO().CarregaUsuarioAD(usuario);

            jsonResultado.Data = usuario;

            return Json(jsonResultado);
        }
    }
}
