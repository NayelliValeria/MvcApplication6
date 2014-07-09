var table;
var aPos;
var aData;
var idCandidato = null;

$(document).ready(function () {
    //Inicializar tabla
    table = $('#candidatos').dataTable({
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
        
    var idCandidato = null;
    //Seleccionar fila-candidato
    $("#candidatos tbody").delegate('tr', 'click', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
            idCandidato = null;
        } else {
            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
            aPos = table.fnGetPosition(this);
            aData = table.fnGetData(aPos[0]);
            idCandidato = aData[aPos][10];
        }
    });

    //Editar
    $("#editarCandidato").on('click', function () {
        if (idCandidato == null)
            window.alert("Seleccionar un candidato");
        else
        {
            window.alert("Editaré a un candidato id" + idCandidato);
        }
    });

    

    $('#mostrar_ocultar').change(function () {
        var seleccionados = $('#mostrar_ocultar').multipleSelect('getSelects');
        for (var j = 0; j < 10; j++) {
            var column = $('#candidatos').dataTable().api().column(j);
            if (inArray(j, seleccionados)) 
                column.visible(true);
            else 
                column.visible(false);
        }
    }).multipleSelect();

});	//end function

function inArray( a, array)
{
    for (var i = 0; i < array.length; i++)
        if (array[i] == a)
            return true;
    return false;
}