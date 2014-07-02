
$(document).ready(function () {

    $('#candidatos').dataTable({
        "paginate": true,
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
              { "searchable": true },
              { "searchable": true },
              { "searchable": true },
              { "searchable": true },
              { "searchable": true },
              { "searchable": true },
              { "searchable": true },
              { "searchable": true },
              { "searchable": true },
              { "searchable": true },
              { "targets":[10], "visible": false, "searchable": false }
        ],
        "aaSorting": [[0, "asc"]],
        "bInfo": true,
        "bAutoWidth": true,
        "bLengthChange": true,
        "bFilter": true,
        "bSort": true
    });
    
});	//end function