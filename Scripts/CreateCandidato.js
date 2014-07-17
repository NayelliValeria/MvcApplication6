$(document).ready(function () {

    noPermitir();

    $('#curp').focus();
    document.getElementById('details').style.visibility = 'hidden';

    $("#curp").focus(function () {
        document.getElementById('resultado_curp').style.visibility = 'hidden';
    });

    $('#curp').blur(function () {
        $('.field-validation-error').remove();
        valor = $('#curp').val();
        if (valor != '' && validarCURP(valor))//validar CURP
        {
            $.ajax({//Se verifica si este CURP ya esta registrado
                url: '/Candidato/verificarCURP',
                type: 'POST',
                data: 'curp=' + valor,
                success: function (resultado) {
                    permitir();
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $('#resultado_curp').append('<p id="error" class="field-validation-error">Este CURP ha sido registrado previamente.</p>');
                    document.getElementById('details').style.visibility = 'visible';
                    noPermitir();
                }
            });//fin .ajax()
        } else { //no
            $('#resultado_curp').append('<p id="error" class="field-validation-error">Este CURP no es valido, por favor verifíquelo.</p>');
            document.getElementById('details').style.visibility = 'hidden';
            noPermitir();
        }
    });

    $('#detalles').click(function () {
        window.alert("enviando");
        $.ajax({
            url: '/Candidato/detallesCURP',
            type: 'POST',
            data: 'curp=' + valor,
            dataType: 'html',
            succes: function (candidato) {
                ventana(candidato);
            }, error: function (jqXHR, textStatus, errorThrown) {
                ventana("Se ha producido un error al consulta, intente nuevamente." + textStatus + " -" + jqXHR + " -" + errorThrown);
            }
        });
        return false;
    });

    function ventana(text) {
        alert("fui llamado");
        var v = document.getElementById("ventana");
        dimmer = document.createElement("div");
        $('#info').append('<p>' + text + '</p>');
        dimmer.style.width = window.innerWidth + 'px';
        dimmer.style.height = window.innerHeight + 'px';
        dimmer.className = 'dimmer';
        dimmer.onclick = function () {
            document.body.removeChild(this);
            $("#info").removeAttr("p");
            $("#info").html('');
        };
        document.body.appendChild(dimmer);
        return false;
    }
    $('#btnCerrarVentana').click(function () {
        var v = document.getElementById("ventana");
        document.body.removeChild(dimmer);
        $("#info").html('');
    });
});

function permitir() {
    document.getElementById('resultado_curp').style.visibility = 'visible';
    $('input[type=text],textarea').prop('readonly', false);
    $('input[type=checkbox]').prop('disabled', false);
    $('#detalles').style.visibility = "hidden";
}

function noPermitir() {
    document.getElementById('resultado_curp').style.visibility = 'visible';
    $('input[type=text],textarea').prop('readonly', true);
    $('input[type=checkbox]').prop('disabled',true);
    $('#curp').prop('readonly', false);
}

/*Valida un CURP*/
function validarCURP(cadena) {
    var expresion = /^[A-Z]{4}[0-9]{6}[H,M][A-Z]{5}[0-9]{2}$/;
    return expresion.test(cadena);
}