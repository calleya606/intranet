/// <reference path="~/Content/Scripts/jquery-1.5.1-vsdoc.js" />
/// <reference path="~/Content/Scripts/INTRANET.js" />
$(document).ready(function () {
    $("#btnSalvar").click(function () {
        MODULO.Salvar();
    });
    $("#btnBuscar").click(function () {
        INTRANET.ModalPage(INTRANET.rootApplication + "ModalUsuario", "ModalUsuario", 800, 600);
    });
    $("#btnNovo").click(function () {
        INTRANET.ModalPage(INTRANET.rootApplication + "ModalBuscaAD", "ModalBuscaAD", 500, 300);
    });
});

var MODULO = {
    Salvar: function () {
        var json = $("form").serializeArray();
        $(".inputError").removeClass("inputError");

        INTRANET.Ajax.GetData("Usuario/Salvar", json, function (retorno) {
            //Verifica se existem criticas a serem exibidas
            if (retorno.Criticas.length > 0) {
                $(retorno.Criticas).each(function () {
                    $("#" + this.FieldId).addClass("inputError");
                    INTRANET.AddToolTip($("#" + this.FieldId), this.Message);
                });
                if (retorno.Message.length > 0) {
                    INTRANET.ShowMessage(retorno.Message, "error");
                }
            }
            else {
                if (retorno.Message.length > 0) {
                    INTRANET.ShowMessage(retorno.Message, "success");
                }
            }
        });
    },
    CarregarRegistro: function () {
        var json = $("form").serializeArray();
        $(".chkPerfil").removeAttr("checked");
        INTRANET.Ajax.GetData("Usuario/CarregarRegistro", json, function (retorno) {
            if (retorno.Data != null) {
                $("#txtNome").val(retorno.Data.Name);
                $("#txtUserName").val(retorno.Data.UserName);
                $("#txtDataExpiracao").val(retorno.Data.ExpirationDate);
                if (retorno.Data.Active) {
                    $("#chkAtivo").attr("checked", "checked");
                }
                else {
                    $("#chkAtivo").removeAttr("checked");
                }
                $(".chkPerfil").each(function () {
                    var chk = this;
                    $(retorno.Data.PerfilIds).each(function () {
                        if ($(chk).val() == this) {
                            $(chk).attr("checked", "checked");
                        }
                    });
                });
            }
        });
    }
};

