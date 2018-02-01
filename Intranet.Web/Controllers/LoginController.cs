using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using OfficeOpenXml.Drawing.Chart;
using iTextSharp.text;
using Svg;
using iTextSharp.text.pdf;
using System.IO;
using System.Text;
using Intranet.ExtensionMethods;

namespace Intranet.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            if (Helpers.Sessions.MensagemLogin != null)
            {
                ViewData["Message"] = Helpers.Sessions.MensagemLogin;
            }

            Session.Abandon();

            return View();
        }

        [HttpPost]
        public ActionResult Index(string UserName, string Password)
        {
            Data.Entities.User adUser = new Data.Entities.User();
            adUser.UserName = UserName;
            adUser.Password = Password;

            var user = new Data.ADO.UserADO().Login(adUser);

            if (user == null)
            {
                ViewData["Message"] = Resources.Geral.UsuarioSenhaIncorreto;
                return View();
            }

            if (!user.Administrator)
            {
                if (user.PerfilIds.Count.Equals(0))
                {
                    ViewData["Message"] = Resources.Geral.UsuarioSemPerfil;
                    return View();
                }

                if (!user.Active)
                {
                    ViewData["Message"] = Resources.Geral.UsuarioInativo;
                    return View();
                }

                if (user.ExpirationDate != null && user.ExpirationDate <= DateTime.Today)
                {
                    ViewData["Message"] = Resources.Geral.UsuarioExpirado;
                    return View();
                }
            }

            //LOG DE ACESSOS
            new Data.ADO.HistoryLoginADO().Log(new Data.Entities.HistoryLogin()
            {
                UserName = user.UserName,
                AccessDate = DateTime.Now,
                UserAgent = Request.UserAgent,
                Ip = Request.ServerVariables["REMOTE_ADDR"].ToString()
            });

            user.Menus = new Intranet.Data.ADO.MenuADO(user).LoadById(null);

            Helpers.Sessions.ADUser = user;

            return RedirectToAction("Index", "Home");
        }
    }
}
