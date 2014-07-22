var table;
var aPos;
var aData;

$(document).ready(function () {
    //Inicializar tabla
    table = $('#tecnologias').dataTable(
        {
            "scrollX": true,
            "paging": true,
            "paginateType": "full_numbers",
            "language": {
                "search": "Buscar:",
                "infoEmpty": "",
                "zeroRecords": "No se encontraron tecnologías registrados",
                "lengthMenu": 'Mostrar _MENU_ registros',
                "paginate":
                {
                    "sNext": "Siguiente",
                    "sPrevious": "Anterior"
                },
                "infoFiltered": " - resultados de  _MAX_ ",
                "info": "Página _PAGE_ de _PAGES_"
            },
            "columnDefs": [
                  { "targets": [1], "visible": false, "searchable": false },
                  { "targets": [2], "sWidth":"20%","searchable": false}
            ],
            "aaSorting": [[0, "asc"]]
        });

    //Seleccionar fila-candidato y mostrar detalles
    $("#tecnologias tbody").delegate('tr', 'click', function () {
        var tr = $(this).closest('tr');
        var row = $('#tecnologias').dataTable().api().row(tr);
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        } else {
            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });

});	//end function