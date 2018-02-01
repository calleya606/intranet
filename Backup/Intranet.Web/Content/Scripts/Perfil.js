/// <reference path="~/Content/Scripts/jquery-1.5.1-vsdoc.js" />
/// <reference path="~/Content/Scripts/INTRANET.js" />
$(document).ready(function () {
    $("#btnSalvar").click(function () {
        MODULO.Salvar();
    });
    $("#btnBuscar").click(function () {
        INTRANET.ModalPage(INTRANET.rootApplication + "ModalPerfil", "ModalPerfil", 450, 300);
    });
    $(".inputChkParent").change(function () {
        $(this).parent().parent().find(".inputChk").removeAttr("checked");
        if ($(this).attr("checked")) {
            $(this).parent().parent().find(".inputChk").attr("checked", "checked");
        }
    });
    $(".inputChk").change(function () {
        var qtd1 = $($(this).parents(".divProfileModules")).find(".inputChk:checked").length;
        if (qtd1 > 0) {
            $(this).parent().parent().parent().find(".inputChkParent").attr("checked", "checked");
        }
        else {
            $(this).parent().parent().parent().find(".inputChkParent").removeAttr("checked");
        }
    });
});

var MODULO = {
    Salvar: function () {
        $(".chkDisabled").removeAttr("disabled");
        var json = $("form").serializeArray();
        $(".inputError").removeClass("inputError");
        INTRANET.Ajax.GetData("Perfil/Salvar", json, function (retorno) {
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

            $(".chkDisabled").attr("disabled", "disabled");
        });
    },
    CarregarRegistro: function () {
        var json = $("form").serializeArray();
        $(".inputChkParent, .inputChk").removeAttr("checked");
        INTRANET.Ajax.GetData("Perfil/CarregarRegistro", json, function (retorno) {
            $("#txtNome").val(retorno.Data.Nome);
            $(".inputChkParent, .inputChk").each(function () {
                var chk = this;
                $(retorno.Data.PerfilIds).each(function () {
                    if ($(chk).val() == this) {
                        $(chk).attr("checked", "checked");
                    }
                });
            });
        });
    }
};


