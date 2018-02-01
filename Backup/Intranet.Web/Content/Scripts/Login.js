/// <reference path="~/Content/Scripts/jquery-1.5.1-vsdoc.js" />

$(document).ready(function () {
    $("input:submit, input:button").button();
    
    $(":text").addClass("inputText");
    $(":text").focusin(function () {
        $(this).addClass("txtOn");
    });
    
    $(":text").focusout(function () {
        $(this).removeClass("txtOn");
    });
    
    $(":password").addClass("inputText");
    $(":password").focusin(function () {
        $(this).addClass("txtOn");
    });
    
    $(":password").focusout(function () {
        $(this).removeClass("txtOn");
    });

    $("#UserName").focus();
});

