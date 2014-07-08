
$(document).ready(function () {
    //Inicializar tabla
    $('#candidatos').dataTable({
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
              { "targets":[10], "visible": false, "searchable": false }
        ],
        "aaSorting": [[0, "asc"]],
        "bInfo": true,
        "bAutoWidth": true,
        "bLengthChange": true,
        "bFilter": true,
        "bSort": true
    });
    
    var table = $('#candidatos').dataTable();

    //Ocultar y mostrar columnas
    $('a.toogle-vis').on('click', function (e) {
        window.alert("visibility: " + column.visible());
        e.preventDefault();
        var column = table.column($(this).attr('data-column'));
        column.visible(!column.visible());
    });
    
    var idCandidato = null;
    //Seleccionar fila-candidato
    $("#candidatos tbody").on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
            idCandidato = null;
        } else {
            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });

    //Editar
    //$("#editarCandidato").on('click', function () { });
    
});	//end function