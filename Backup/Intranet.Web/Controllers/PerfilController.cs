using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intranet.ExtensionMethods;

namespace Intranet.Controllers
{
    public class PerfilController : BaseController
    {
        public ActionResult Index()
        {
            VerificaPermissao(this.RouteData.Values["controller"].ToString());

            return View();
        }

        public JsonResult Salvar(FormCollection form)
        {
            Entities.JsonReturnJS jsonResultado = new Entities.JsonReturnJS();
            Data.Entities.Perfil perfil = new Data.Entities.Perfil();

            try
            {
                ValidaForm(form, jsonResultado);

                if (jsonResultado.Criticas.Count > 0)
                {
                    jsonResultado.Message = Intranet.Resources.Geral.VerifiqueCritica;
                    return Json(jsonResultado);
                }

                perfil.Id = form["hdnId"].ToInteger();
                perfil.Nome = form["txtNome"].ToString().Trim();

                string[] perfilIds = form.GetValues("chkMenu");

                if (perfilIds != null)
                {
                    foreach (var strId in perfilIds)
                    {
                        perfil.PerfilIds.Add(strId.ToInteger());
                    }
                }

                new Data.ADO.BinaryItensADO().Salvar(perfil, Helpers.Sessions.ADUser);

                if (perfil.Id.Equals(0))
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
            var perfil = new Data.ADO.BinaryItensADO().Carregar<Data.Entities.Perfil>(form["hdnId"].ToInteger());
            jsonResultado.Data = perfil;
            return Json(jsonResultado);
        }

        private void ValidaForm(FormCollection form, Entities.JsonReturnJS jsonResultado)
        {
            if (form["txtNome"].Trim().Length.Equals(0))
            {
                jsonResultado.Criticas.Add(new Entities.JsonCriticaJS() { FieldId = "txtNome", Message = "Informe o nome." });
            }
            else
            {
                if (new Data.ADO.BinaryItensADO().CarregarTudo<Data.Entities.Perfil>().Count(n => n.Nome.Equals(form["txtNome"].Trim()) && n.Id != form["hdnId"].ToInteger()) > 0)
                {
                    jsonResultado.Criticas.Add(new Entities.JsonCriticaJS() { FieldId = "txtNome", Message = "Já existe um perfil com este nome no sistema." });
                }
            }
        }
    }
}
