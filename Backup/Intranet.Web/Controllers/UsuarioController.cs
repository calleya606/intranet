using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intranet.ExtensionMethods;

namespace Intranet.Controllers
{
    public class UsuarioController : BaseController
    {
        //
        // GET: /Usuario/
        public ActionResult Index()
        {
            VerificaPermissao(this.RouteData.Values["controller"].ToString());

            ViewBag.Perfis = new Data.ADO.BinaryItensADO().CarregarTudo<Data.Entities.Perfil>().OrderBy(n => n.Nome).ToList();

            return View();
        }

        public JsonResult Salvar(FormCollection form)
        {
            Entities.JsonReturnJS jsonResultado = new Entities.JsonReturnJS();
            Data.Entities.User usuario = new Data.Entities.User();
            bool novo = false;

            try
            {
                ValidaForm(form, jsonResultado);

                if (jsonResultado.Criticas.Count > 0)
                {
                    jsonResultado.Message = Resources.Geral.VerifiqueCritica;
                    return Json(jsonResultado);
                }

                var usuariosTemp = new Data.ADO.BinaryItensADO().CarregarTudo<Data.Entities.User>().Where(n => n.UserName == form["txtUserName"]).ToList();
                if (usuariosTemp.Count > 0)
                {
                    usuario = usuariosTemp.First();
                }
                else
                {
                    novo = true;
                }

                usuario.Name = form["txtNome"];
                usuario.UserName = form["txtUserName"];

                if (!string.IsNullOrEmpty(form["txtDataExpiracao"].Trim()))
                    usuario.ExpirationDate = Convert.ToDateTime(form["txtDataExpiracao"]);
                usuario.Active = form["chkAtivo"] == null ? false : true;

                //PERFIS
                string[] perfilIds = form.GetValues("chkPerfil");
                if (perfilIds != null)
                {
                    usuario.PerfilIds = new List<int>();
                    foreach (var id in perfilIds)
                    {
                        usuario.PerfilIds.Add(id.ToInteger());
                    }
                }

                new Data.ADO.BinaryItensADO().SalvarUsuario(usuario, Helpers.Sessions.ADUser);

                if (novo)
                {
                    jsonResultado.Message = Intranet.Resources.Geral.RegistroInseridoSucesso;
                }
                else
                {
                    jsonResultado.Message = Intranet.Resources.Geral.RegistroAtualizadoSucesso;
                }
            }
            catch (Exception ex)
            {
                jsonResultado.ErrorMessage = ex.Message;
            }

            return Json(jsonResultado);
        }

        public JsonResult CarregarRegistro(FormCollection form)
        {
            Entities.JsonReturnJS jsonResultado = new Entities.JsonReturnJS();
            var usuarios = new Data.ADO.BinaryItensADO().CarregarTudo<Data.Entities.User>().Where(n => n.UserName == form["txtUserName"]).ToList();
            if (usuarios.Count > 0)
            {
                jsonResultado.Data = usuarios.First();
            }
            return Json(jsonResultado);
        }

        private void ValidaForm(FormCollection form, Entities.JsonReturnJS jsonResultado)
        {
            if (form["txtNome"].Trim().Length.Equals(0))
            {
                jsonResultado.Criticas.Add(new Entities.JsonCriticaJS() { FieldId = "txtNome", Message = "Informe o nome." });
            }
            if (form["txtUserName"].Trim().Length.Equals(0))
            {
                jsonResultado.Criticas.Add(new Entities.JsonCriticaJS() { FieldId = "txtUserName", Message = "Informe o login." });
            }
        }
    }
}
