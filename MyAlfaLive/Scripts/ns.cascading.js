// #region generic Ajax functions

function genericError(data) {
    var html = $(data.responseText);
    var title = $(html)[1];
    var error = $(title).text();

    alert('Error en el sistema:\n\n' +
        data.status + ' ' + data.statusText +
        '\n' + error + '\n\n' + 'Intente nuevamente');
}

// #endregion

// #region Cascading dropdown lists

/**
 * Cascading dropdown lists
 * 1. Disable Child dropdowns: disbled = "disabled"
 * 2. Create function for aditional params [*Optional]
 *  function params() {
        return {
            idCiudad: $("#IdCiudad").val(),
            idTipoArea: $('#IdTipoArea').val()
        };
    }

    3. ON LOAD parse form before call ajax service 
       $.validator.unobtrusive.parse($("form"));

    4.- Call method
       var urlTipoArea = '@Url.Action("TipoArea", "DropDown", new { area = "" })';
       var urlArea = '@Url.Action("AreasPorCiudadYTipo", "DropDown", new { area = "" })';

       cascading("IdCiudad", "IdTipoArea", urlTipoArea, null); // enable cascading dropdownlists, without additional params
       cascading("IdTipoArea", "IdArea", urlArea, params); // pasing function for additional params
 */
function cascading(parentId, childId, url, additionalData) {
    var parent = $('#' + parentId);
    var child = $('#' + childId);

    // Si los combos tienen valores seleccionados desbloquear hijo.
    if (parent.val() && child.val()) {
        child.prop('disabled', false);
    }
    else {
        child.prop('disabled', true);
    }

    // Si el padre tiene valor y el hijo no, cargar items del hijo
    if (parent.val() && !child.val()) {
        loadItems(parentId, childId, url, additionalData);
    }

    // Bind change event
    parent.on('change', function () {
        loadItems(parentId, childId, url, additionalData);
    });
}

function loadItems(parentId, childId, url, additionalData) {
    var parentVal = $('#' + parentId).val();
    var cbxChild = $('#' + childId);
    var childVal = cbxChild.val();

    // Clean child items & trigger change event for childs
    cbxChild.empty();
    cbxChild.change()

    // Add default item
    cbxChild.append($('<option>', {
        value: '',
        text: '< seleccione >'
    }));

    // Si el padre tiene seleccionado un valor
    if (parentVal) {

        // Loading para hijo  
        var opts = { lines: 10, length: 4, width: 2, radius: 5 };
        var spinner = new Spinner(opts).spin();

        var extraData = null;

        // if additionalData is function
        if (additionalData && (typeof additionalData === "function")) {
            // Call function
            extraData = additionalData();
        }
        else {
            // pass as object
            extraData = additionalData;
        }

        // ajax call
        $.ajax({
            type: "POST",
            url: url + '/' + parentVal,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(extraData),
            beforeSend: function () {
                cbxChild.parent("div").append(spinner.el);
            },
            success: function (response) {
                // Add child items
                if (response.length > 0) {
                    $.each(response, function (index, item) {
                        cbxChild.append($('<option>', {
                            value: item.Id,
                            text: item.Text
                        }));
                    });
                    cbxChild.prop('disabled', false);
                }
                // Enable child
                spinner.stop();
            },
            error: function (data) {
                spinner.stop();
                genericError(data);
            }
        });
    }
    else {
        // Disable child
        cbxChild.change();
        cbxChild.prop('disabled', 'disabled');
    }
}

// #endregion