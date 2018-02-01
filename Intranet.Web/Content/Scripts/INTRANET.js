/// <reference path="~/Content/Scripts/jquery-1.5.1-vsdoc.js" />

$(document).ready(function () {

    INTRANET.EnableWaiting = true;

    $("input:submit, input:button, a.button").button();
    $("#btnSalvar").button({ icons: { primary: "ui-icon-disk" }, text: true, disabled: false });
    $("#btnNovo").button({ icons: { primary: "ui-icon-document" }, text: true, disabled: false });
    $("#btnBuscar").button({ icons: { primary: "ui-icon-search" }, text: true, disabled: false });
    $(".buttonDisabled").button({ disabled: true });
    $(".tabs").tabs();
    $(".date").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true
    });

    var showWait = false;

    //BEGIN Loading AJAX ####################################################################
    var timeOutWainting = null;
    $(document).ajaxStart(function () {
        if (INTRANET.EnableWaiting = true) {
            showWait = true;
            window.setTimeout(function () {
                if (showWait) {
                    INTRANET.waiting();
                }
            }, 250);
        }
    }).ajaxStop(function () {
        if (INTRANET.EnableWaiting = true) {
            showWait = false;
            $.unblockUI();
        }
    });
    //END Loading AJAX ######################################################################

    INTRANET.SetMasks();
    INTRANET.AdicionaFuncionalidadesTextBox();
    INTRANET.SetMasks();

    $("#divContent").fadeIn(1000);
    $("#barraFerramentas").fadeIn(1000);
    
    setInterval(function () {
        INTRANET.VerifySystemMessage();
    }, 30000);

    INTRANET.AdicionaEventoLinhaTabela();
});

var INTRANET = {

    EnableWaiting: true,
    rootApplication: "", //Setado na master
    ShowSystemMessage: false,

    AdicionaEventoLinhaTabela: function () {
        $("#tblListGeral").mouseleave(function () {
            $("#tblListGeral tbody tr td").removeClass("trOn");
        });
        $("#tblListGeral tbody tr td").mouseover(function () {
            $("#tblListGeral tbody tr td").removeClass("trOn");
            $($(this).parents("tr")).find("td").addClass("trOn");
        });
    },

    VerifySystemMessage: function () {
        INTRANET.Ajax.GetData("AjaxMethods/VerifySystemMessage", {}, function (jsonReturn) {
            if (jsonReturn.Data.RedirectUrl.length > 0) {
                window.location.href = jsonReturn.Data.RedirectUrl;
                return;
            }

            if (jsonReturn.Data.Message.length > 0 && !INTRANET.ShowSystemMessage) {
                INTRANET.ShowSystemMessage = true;
                INTRANET.MessageBox(jsonReturn.Data.Message);
            }
        });
    },

    AddHightLightObject: function (obj) {
        $(obj).animate({ backgroundColor: "yellow" }, 500, function () {
            $(this).animate({ backgroundColor: "transparent" }, 500);
        });
    },

    AdicionaFuncionalidadesTextBox: function () {
        $(":text").focusin(function () {
            $(this).removeClass("txtOn").addClass("txtOn");
        });
        $(":text").focusout(function () {
            $(this).removeClass("txtOn");
        });
        $("textarea").focusin(function () {
            $(this).removeClass("txtOn").addClass("txtOn");
        });
        $("textarea").focusout(function () {
            $(this).removeClass("txtOn");
        });
    },

    ModalPage: function (url, modalId, w, h) {
        if (w == null)
            w = 400;
        if (h == null)
            h = 300;
        $("<div class=\"" + modalId + "\"><iframe src=\"" + url + "\" frameborder=\"0\" width=\"" + (w - 25) + "px\" height=\"" + (h - 40) + "px\" /></div>").dialog({ width: w, height: h, resizable: false, modal: true });
    },

    ModalReports: function (url, modalId) {

        $("<div class=\"" + modalId + "\"><iframe src=\"" + url + "\" frameborder=\"0\" width=\"775px\" height=\"560px\" /></div>").dialog({ width: 800, height: 600, resizable: false, modal: true });

    },

    ModalElement: function (objId, w, h) {
        if (w == null && h == null) {
            w = 500;
            h = 400;
        }
        $("#" + objId).dialog({ width: w, height: h, resizable: false, modal: false });
    },

    MessageBox: function (message) {

        $("#spanMessageBox").html(message);

        $("#dialog-message").dialog({
            modal: true,
            buttons: {
                Ok: function () {
                    $(this).dialog("close");
                }
            }
        });

    },

    ShowMessage: function (message, type) {
        //alert
        //information
        //error
        //warning
        //notification
        //success
        var n = noty({
            text: "<strong>" + message + "</strong>",
            type: type,
            dismissQueue: true,
            layout: 'topCenter',
            theme: 'defaultTheme'
        });
        window.setTimeout(function () {
            n.close();
        }, 2000);
    },

    AddToolTip: function (obj, message, blockToolTip) {
        if (blockToolTip == true) {
            $(obj).tooltip({
                delay: 500,
                showURL: false
            });
        }
        else {
            $(obj).tooltip({
                delay: 500,
                bodyHandler: function () {
                    return message;
                },
                showURL: false
            });
        }
    },

    SetMasks: function () {

        //MASK CONFIG
        $(':text.cpf').setMask('cpf');
        $(':text.cnpj').setMask('cnpj');
        $(':text.rg').setMask({ mask: '9999999999' });
        $(':text.cep').setMask({ mask: '99999-999' });
        $(':text.dateFormat').setMask({ mask: '39/19/9999' });
        $(':text.time').setMask({ mask: '29:69' });
        $(':text.ddd').setMask({ mask: '99' });
        $(':text.cep').setMask({ mask: '99999999' });
        $(':text.fone').setMask({ mask: '(99) 9999-9999' });
        $(':text.number').setMask({ mask: '9', type: 'repeat' });
        $(':text.money').setMask({ mask: '99,999.999.999.999', type: 'reverse', defaultValue: '000' });
        $(':text.decimal').setMask({ mask: '99,999.999.999.999', type: 'reverse', defaultValue: '+000' });


    },

    waiting: function () {

        var html = "";
        html += "<div style=\"z-index: 999999;padding: 10px;border: 5px solid #ccc;-webkit-border-radius: 10px;-moz-border-radius: 10px;background-color: #fff;\">";
        html += "   <div style=\"font-weight: bold;\">";
        html += "       <div style=\"overflow: hidden;\">";
        html += "           <div style=\"float: left; margin-left: 10px;\">";
        html += "               <img src=\"" + INTRANET.rootApplication + "content/Images/ajax-loader.gif\" />";
        html += "           </div>";
        html += "           <div style=\"float: left; margin-left: 10px; padding-top: 2px;\">";
        html += "               Aguarde...";
        html += "           </div>";
        html += "       </div>";
        html += "   </div>";
        html += "</div>";

        $.blockUI({
            message: html,
            overlayCSS: {
                backgroundColor: '#000',
                opacity: 0.3
            },
            css: {
                border: 'none',
                left: ($(window).width() - 150) / 2 + 'px',
                width: '150px',
                backgroundColor: 'transparent'
            }
        });
    },

    Ajax: function () {
        return {
            Name: '',
            GetData: function (url, jsonData, callbackSucesso, async) {

                var asyncTemp = true;
                if (async != null && async != undefined) {
                    asyncTemp = async;
                }

                $.ajax({
                    type: "POST",
                    async: asyncTemp,
                    data: jsonData,
                    url: INTRANET.rootApplication + url,
                    success: function (resposta) {
                        if (resposta.ErrorMessage != "") {
                            INTRANET.MessageBox(resposta.ErrorMessage);
                        }
                        else {
                            if (jQuery.isFunction(callbackSucesso)) {
                                callbackSucesso(resposta)
                            }
                        }
                    },
                    error: function (resposta) {
                        INTRANET.MessageBox(resposta.responseText);
                    }
                });
            },

            GetView: function (url, jsonData, callbackSucesso) {
                $.ajax({
                    type: "POST",
                    data: jsonData,
                    async: false,
                    url: INTRANET.rootApplication + url,
                    success: function (resposta) {
                        if (jQuery.isFunction(callbackSucesso)) {
                            callbackSucesso(resposta)
                        }
                    },
                    error: function (resposta) {
                        INTRANET.MessageBox(resposta.responseText);
                    }
                });
            }
        }
    }()

};