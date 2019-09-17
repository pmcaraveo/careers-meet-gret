// Author: Nestor Alonzo
// Spanish language. Para datatables en general
// "Spanish.json" Para Mvc.Jquery.DataTables
var languageSpanish = {
	"sProcessing": "Procesando...",
	"sLengthMenu": "Mostrar _MENU_ registros",
	"sZeroRecords": "No se encontraron resultados",
	"sEmptyTable": "Ningún dato disponible en esta tabla",
	"sInfo": "Mostrando _START_ a _END_ de _TOTAL_",
	"sInfoEmpty": "Mostrando 0 a 0 de 0 ",
	"sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
	"sInfoPostFix": "",
	"sSearch": "Buscar:",
	"sUrl": "",
	"sInfoThousands": ",",
	"sLoadingRecords": "Cargando...",
	"oPaginate": {
		"sFirst": "<<",
		"sLast": ">>",
		"sNext": ">",
		"sPrevious": "<"
	},
	"oAria": {
		"sSortAscending": ": Activar para ordenar la columna de manera ascendente",
		"sSortDescending": ": Activar para ordenar la columna de manera descendente"
	}
};

// Mini DataTable (Client side order and pagination)
function DataTable_Mini(table) {
	return $(table).DataTable({
	    language: languageSpanish,
	    pageLength: 5,
		dom: "rtip" //"lfrtip" Hide search box and page length
	});
}

// Column type Enum
var COLUMN_TYPE = {
	DATE: 'date',
	NUMBER: 'num',
	NUMBER_FORMATTED: 'num-fmt',
	STRING: 'string'
};

var showError = function (jqXHR, textStatus, errorThrown) {
    console.log('error loading data: ' + textStatus + ' - ' + errorThrown);
    console.log(arguments);
    var message = 'Error en el sistema:\n\n' +
            textStatus + ': ' + errorThrown + '\n\n' +
            'INTENTE NUEVAMENTE. Si persiste el error, contacte a su administrador.';

    var process = $('#tblAudiencias_processing');
    if (process) {
        process.html('<span class="fa fa-exclamation-circle" style="color: red;"></span> ' + message.replace(/\n/g,'<br>'));
    }
    else {
        alert(message);
    }
    
};