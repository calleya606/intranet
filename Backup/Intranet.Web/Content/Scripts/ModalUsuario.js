/// <reference path="~/Content/Scripts/jquery-1.5.1-vsdoc.js" />
/// <reference path="~/Content/Scripts/INTRANET.js" />
$(document).ready(function () {
    MODULO.CarregarTudo();
});

var MODULO = {
    ModuloNome: "ModalUsuario",
    CarregarTudo: function () {
        var json = $("form").serializeArray();
        var html = "";
        $("#tblListGeral tbody").html(html);
        INTRANET.Ajax.GetData(MODULO.ModuloNome + "/CarregarTudo", json, function (retorno) {
            $(retorno.Data).each(function () {
                html += "<tr>";
                html += "<td>";
                html += "<input type=\"hidden\" id=\"hdnName\" name=\"hdnName\" value=\"" + this.Name + "\" />";
                html += "<input type=\"hidden\" id=\"hdnUserName\" name=\"hdnUserName\" value=\"" + this.UserName + "\" />";
                html += this.Name;
                html += "</td>";
                html += "<td>";
                html += this.UserName;
                html += "</td>";
                html += "</tr>";
            });
            $("#tblListGeral tbody").html(html);

            $("#tblListGeral tbody tr td").unbind("click");
            $("#tblListGeral tbody tr td").click(function () {
                var userName = $($(this).parents("tr")).find("#hdnUserName").val();
                var name = $($(this).parents("tr")).find("#hdnName").val();
                parent.$("#txtNome").val(name);
                parent.$("#txtUserName").val(userName);
                parent.MODULO.CarregarRegistro();
                parent.$("." + MODULO.ModuloNome).dialog("close");
            });

            INTRANET.AdicionaEventoLinhaTabela();
        });
    }
};

