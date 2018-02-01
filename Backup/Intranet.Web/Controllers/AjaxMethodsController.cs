using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intranet.Data.Entities;
using Intranet.Entities;

namespace Intranet.Controllers
{
    public class AjaxMethodsController : BaseController
    {
        //TODO: Implementar mensagem de atualização do sistema.
        public JsonResult VerifySystemMessage()
        {
            JsonReturnJS jsonRetornoJS = new JsonReturnJS();
            SystemMessage systemMessage = new SystemMessage();

            if (Helpers.Sessions.ADUser == null)
            {
                systemMessage.RedirectUrl = Helpers.AppSettings.RootApplication;
                jsonRetornoJS.Data = systemMessage;
                return Json(jsonRetornoJS);
            }

            //systemMessage.SystemMessageType = Data.Enumerators.SystemMessageType.Information;
            systemMessage.Message = new Intranet.Data.ADO.SystemMessageADO().GetMessage();

            if (systemMessage.Message.Length > 0)
            {
                jsonRetornoJS.Data = systemMessage;
                return Json(jsonRetornoJS);
            }

            jsonRetornoJS.Data = systemMessage;
            return Json(jsonRetornoJS);
        }

    }
}
