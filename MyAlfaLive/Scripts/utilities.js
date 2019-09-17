
// Si el país es México muestra 
// los controles para solicitar la Entidad Federativa
function ToggleEntidad(paisId, toggleDivId) {
    if (paisId == 126) {
        // Mostrar Curp
        $('#' + toggleDivId).show(300);
    }
    else {
        // Ocultar
        $('#' + toggleDivId).hide(300);
    }
}

function ToggleDatosGenerales(nacionalidadId) {
    // Nacionalidad MEXICANA
    if (nacionalidadId == 126) {
        // Mostrar Curp
        $('#divNumeroPasaporte').hide(300);
        $('#divCURP').show(300);
    }
    else if (nacionalidadId == '') {
        // NO SELECCIONADO
        //Ocultar todos
        $('#divNumeroPasaporte').hide(300);
        $('#divCURP').hide(300);
    }
    else {
        // != MEXICANA
        // Mostrar NumeroPasaporte
        $('#divCURP').hide(300);
        $('#divNumeroPasaporte').show(300);
    }
}

function spinOpts() {
    return { lines: 10, length: 4, width: 2, radius: 5 };
}

function genericAjaxCall(url, targetId) {
    $.ajax({
        type: 'GET',
        url: url,
        beforeSend: function () {
            onBeforeSend(targetId);
        },
        success: function (data) {
            onSuccess(data, targetId);
        },
        error: function (data) {
            onError(data, targetId);
        }
    });
}

function onBeforeSend(targetId) {
    // Create spinner
    $("#" + targetId).spin(spinOpts());
}

function onSuccess(data, targetId) {
    // Delete spinner
    $("#" + targetId).spin(false);
    $('#' + targetId).html(data).hide().slideDown(500);
}

function onError(data, targetId) {
    genericError(data);
    // Delete spinner
    $("#" + targetId).spin(false);
}

function genericError(data) {
    var html = $(data.responseText);
    var title = $(html)[1];
    var error = $(title).text();

    alert('Error en el sistema:\n\n' +
        data.status + ' ' + data.statusText +
        '\n' + error + '\n\n' + 'Intente nuevamente');
}

function moneyConfig() {
    return {
        'groupSeparator': ',',
        'prefix': '$ ',
        'autoGroup': true,
        'autoUnmask': true,
        'removeMaskOnSubmit': true
    };
}

function integerConfig() {
    return {
        'groupSeparator': ',',
        'autoGroup': true,
        'autoUnmask': true,
        'removeMaskOnSubmit': true,
        'digits': 0
    };
}

// DataTableHelper
// Remove the formatting to get integer data for summation
var intVal = function (i) {
    return typeof i === 'string' ?
        i.replace(/[\$,]/g, '') * 1 :
        typeof i === 'number' ?
            i : 0;
};

var formatCurrency = function (value) {
    return '$' + parseFloat(value, 10).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,");
};


// Select 2 utilities methods

function Select2AjaxOptions(url) {
    return {
        url: url,
        dataType: 'json',
        delay: 250,
        data: function (params) {
            return {
                q: params.term
            };
        },
        processResults: function (data) {
            return {
                results: data
            };
        }
    };
}

function MySelect2(selector, url) {
    $(selector).select2({
        minimumInputLength: 3,
        theme: 'bootstrap',
        language: 'es',
        width: '280px',
        placeholder: 'buscar...',
        allowClear: true,
        ajax: Select2AjaxOptions(url)
    });
}

// Show first tab with control errors in it
function showTabWithErrors() {
    if ($(".tab-content").find("div.tab-pane.active:has(.form-control.input-validation-error)").length == 0) {
        var id = $(".tab-content").find("div.tab-pane:has(.form-control.input-validation-error)").first().attr("id");
        $('a[href="#' + id + '"]').tab('show');
    }
}

function initializeCreatePersonaFisicaControls(municipioUrl)
{
    // Select2
    $('#PaisNacimientoId, #NacionalidadId,' +
        '#PaisResidenciaId, #ActividadId,' +
        '#MunicipioNacimientoId, #MunicipioResidenciaId,' +
        '#EntidadOperacionId, #EntidadNacimientoId,' +
        '#EntidadResidenciaId, #MunicipioOperacionId').select2({
            language: 'es',
            width: '280px',
            theme: "bootstrap"
        });

    $('#PerfilTransaccional, #Productos').select2({
        language: 'es',
        width: '280px',
        multiple: true,
        theme: "bootstrap"
    });

    // Toggle on load
    ToggleEntidad($("#PaisNacimientoId").val(), 'divEntidadNacimiento');
    ToggleEntidad($("#PaisResidenciaId").val(), 'divEntidadResidencia');
    ToggleDatosGenerales($("#NacionalidadId").val());

    // Bind change events
    $('#PaisNacimientoId').on('select2:select', function (e) {
        ToggleEntidad(e.params.data.id, 'divEntidadNacimiento');
    });

    $('#NacionalidadId').on('select2:select', function (e) {
        ToggleDatosGenerales(e.params.data.id);
    });

    $('#PaisResidenciaId').on('select2:select', function (e) {
        ToggleEntidad(e.params.data.id, 'divEntidadResidencia');
    });

    cascading("EntidadNacimientoId", "MunicipioNacimientoId", municipioUrl);
    cascading("EntidadResidenciaId", "MunicipioResidenciaId", municipioUrl);
    cascading("EntidadOperacionId", "MunicipioOperacionId", municipioUrl);

    // Next buttons
    $('.next-button').on('click', function (e) {
        e.preventDefault();
        var next_tab = $('.nav-tabs > .active').next('li').find('a');
        if (next_tab.length > 0) {
            next_tab.trigger('click');
        } else {
            $('.nav-tabs li:eq(0) a').trigger('click');
        }
    });

    // Validation errors
    $("#frmCreatePersonaFisica").submit(function () {
        showTabWithErrors();
    });

    showTabWithErrors();
}