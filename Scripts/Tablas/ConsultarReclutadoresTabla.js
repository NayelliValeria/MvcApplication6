var table;
var aPos;
var aData;

$(document).ready(function () {
    //Inicializar tabla
    table = $('#reclutadores').dataTable(
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
                  { "targets": [5], "visible": false, "searchable": false },
                  { "targets": [6], "visible": true, "searchable": false }
            ],
            "aaSorting": [[0, "asc"]]
        });

    //Seleccionar fila-candidato y mostrar detalles
    $("#reclutadores tbody").delegate('tr', 'click', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        } else {
            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });
});	//end function