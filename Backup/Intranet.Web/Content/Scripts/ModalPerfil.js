/// <reference path="~/Content/Scripts/jquery-1.5.1-vsdoc.js" />
/// <reference path="~/Content/Scripts/INTRANET.js" />
$(document).ready(function () {
    MODULO.CarregarTudo();
});

var MODULO = {
    ModuloNome: "ModalPerfil",
    CarregarTudo: function () {
        var json = $("form").serializeArray();
        var html = "";
        $("#tblListGeral tbody").html(html);
        INTRANET.Ajax.GetData(MODULO.ModuloNome + "/CarregarTudo", json, function (retorno) {
            $(retorno.Data).each(function () {
                html += "<tr>";
                html += "<td>";
                html += "<input type=\"hidden\" id=\"hdnId\" name=\"hdnId\" value=\"" + this.Id + "\" />";
                html += "<input type=\"hidden\" id=\"hdnUserName\" name=\"hdnUserName\" value=\"" + this.UserName + "\" />";
                html += this.Nome;
                html += "</td>";
                html += "</tr>";
            });
            $("#tblListGeral tbody").html(html);

            $("#tblListGeral tbody tr td").unbind("click");
            $("#tblListGeral tbody tr td").click(function () {
                var hdnId = $($(this).parents("tr")).find("#hdnId").val();
                parent.$("#hdnId").val(hdnId);
                parent.MODULO.CarregarRegistro();
                parent.$("." + MODULO.ModuloNome).dialog("close");
            });

            INTRANET.AdicionaEventoLinhaTabela();
        });
    }
};

