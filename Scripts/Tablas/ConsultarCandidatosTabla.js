var table;
var aPos;
var aData;

$(document).ready(function () {
    //Inicializar tabla
    table = $('#candidatos').dataTable(
        {
            "scrollX": true,
            "paging": true,
            "paginateType": "full_numbers",
            "language": {
                "search": "Buscar:",
                "infoEmpty": "",
                "zeroRecords": "No se encontraron reclutadores registrados",
                "lengthMenu": 'Mostrar _MENU_ registros',
                "paginate":
                {
                    "sNext": "Siguiente",
                    "sPrevious": "Anterior"
                },
                "infoFiltered": " - resultados de  _MAX_ ",
                "info": "Mostrando _PAGE_ de _PAGES_ resultados"
            },
            "columnDefs": [
                  {"targets": [10], "visible": false, "searchable": false },
                  { "targets":[11], "visible": false, "searchable": false }
            ],
            "aaSorting": [[0, "asc"]]
    });

    //Seleccionar fila-candidato y mostrar detalles
    $("#candidatos tbody").delegate('tr', 'click', function ()
    {
        var tr = $(this).closest('tr');
        var row = $('#candidatos').dataTable().api().row(tr);
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
            row.child.hide();
        } else{
            ocultarTodosDetalles();
            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
            aPos = table.fnGetPosition(this);
            aData = table.fnGetData(aPos[0]);
            if (aData != null)
            {
                var detalles = format(aData[aPos]);
                row.child(detalles).show();
            }
        }
    });

    //Editar
    $("#editarCandidato").on('click', function () {
        if (idCandidato == null)
            window.alert("Seleccionar un candidato");
        else{
            document.getElementById('idCandidato').value = idCandidato;
            editarCandidato();
        }
    });

    $("#mostrar_ocultar").multipleSelect();
    $("#mostrar_ocultar").multipleSelect("setSelects", [0, 1, 2]);
    redefinirColumnas();

    //Mostrar y ocultar columnas
    $('#mostrar_ocultar').change(function () {
        ocultarTodosDetalles();
        redefinirColumnas();
    });

});	//end function

//Buscar elemento en array
function inArray( a, array)
{
    for (var i = 0; i < array.length; i++)
        if (array[i] == a) {
            return true;
        }
    return false;
}

function redefinirColumnas() {
    var seleccionados = $('#mostrar_ocultar').multipleSelect('getSelects');
    for (var j = 0; j < 10; j++) {
        var column = $('#candidatos').dataTable().api().column(j);
        if (inArray(j, seleccionados))
            column.visible(true);
        else
            column.visible(false);
    }
}

function ocultarTodosDetalles() {
    var ant = $('#candidatos').dataTable().api().row(table.$('tr.selected'));
    ant.child.hide();
}

//Establece el formato para la columna de detalles
function format(d) {
    var seleccionados = $('#mostrar_ocultar').multipleSelect('getSelects');
    var details = '<table cellpadding="' + seleccionados.length + '"cellspacing="0" border="0" style="padding-left:50px;">';
    if (seleccionados.length > 0) {
        if (!inArray(0, seleccionados) || !inArray(1, seleccionados) || !inArray(2, seleccionados))
            details +=
                '<tr>' +
                    '<td>Nombre:</td>' +
                    '<td>' + d[0] + ' ' + d[1] + ' ' + d[2] + '</td>' +
                '</tr>';
        if (!inArray(3, seleccionados))
            details +=
                '<tr>' +
                    '<td>CURP:</td>' +
                    '<td>' + d[3] + '</td>' +
                '</tr>';
        if (!inArray(4, seleccionados))
            details +=
                '<tr>' +
                    '<td>RFC:</td>' +
                    '<td>' + d[4] + '</td>' +
                '</tr>';
        if (!inArray(5, seleccionados))
            details +=
                '<tr>' +
                    '<td>e-mail:</td>' +
                    '<td>' + d[5] + '</td>' +
                '</tr>';
        if (!inArray(6, seleccionados))
            details +=
                '<tr>' +
                    '<td>Telefono:</td>' +
                    '<td>' + d[6] + '</td>' +
                '</tr>';
        if (!inArray(7, seleccionados))
            details +=
                '<tr>' +
                    '<td>Tecnologias:</td>' +
                    '<td>' + d[7] + '</td>' +
                '</tr>';
        if (!inArray(8, seleccionados))
            details +=
                '<tr>' +
                    '<td>Palabras clave:</td>' +
                    '<td>' + d[8] + '</td>' +
                '</tr>';
        if (!inArray(9, seleccionados))
            details +=
                '<tr>' +
                    '<td>Fecha de registro:</td>' +
                    '<td>' + d[9] + '</td>' +
                '</tr>';
    }
    details +=
        '<tr>' +
            '<td>' + d[11] + '</td>' +
        '</tr>' +
    '</table>';
    return details;
}