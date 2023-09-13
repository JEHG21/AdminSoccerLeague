FormatedorMensajesError = function (response) {
    if (isJson(response.responseText)) {
        var errores = JSON.parse(response.responseText);
        var message = '<span class="ui-icon ui-icon-alert" ' +
            'style="float:left; margin-right:.3em;"></span>';
        $.each(errores, function (key, value) {
            message += '<li>' + value + '</li>';
        });
        return message
    }
    else
        return '<span class="ui-icon ui-icon-alert" ' +
            'style="float:left; margin-right:.3em;"></span>' +
            response.responseText;
}

AgregarErrorDiv = function (response) {
    $("#errorDiv").empty();
    $("#errorDiv").append('<div class="alert alert-block alert-danger">' +
        '<button type="button" class="close" data-dismiss="alert" >' +
        '<i class="ace-icon fa fa-times red"></i></button> ' +
        FormatedorMensajesError(response) + '</div>'
    );
}

AgregarErrorDivPersonalizado = function (response, control) {
    $(control).empty();
    $(control).append('<div class="alert alert-block alert-danger">' +
        '<button type="button" class="close" data-dismiss="alert" >' +
        '<i class="ace-icon fa fa-times red"></i></button> ' +
        FormatedorMensajesError(response) + '</div>'
    );
}