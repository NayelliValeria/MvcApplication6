$(document).ready(function () {

    $('#curp').focus();
    CURPVacio();
    document.getElementById('details').style.visibility = 'hidden';

    $("#curp").focus(function () {
        document.getElementById('resultado_curp').style.visibility = 'hidden';
        document.getElementById('details').style.visibility = 'hidden';
    });

    $('#curp').blur(function () {
        $('.field-validation-error').remove();
        if (CURPVacio())//validar CURP
        {
            $.ajax({//Se verifica si este CURP ya esta registrado
                url: '/Candidato/verificarCURP',
                type: 'POST',
                data: 'curp=' + valor,
                success: function (resultado) {
                    permitir();
                    $('#persona_nombre').focus();
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $('#resultado_curp').append('<p id="error" class="field-validation-error">Este CURP ha sido registrado previamente.</p>');
                    document.getElementById('details').style.visibility = 'visible';
                    noPermitir();
                }
            });//fin .ajax()
        }
    });

    $('#detalles').click(function () {
        $.ajax({
            url: '/Candidato/detallesCURP',
            type: 'GET',
            data: 'curp=' + valor,
            dataType:"text",
            success: function (candidato) {
                ventana(candidato);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                ventana("Se ha producido un error al consulta, intente nuevamente." + textStatus + " -" + jqXHR + " -" + errorThrown);
            }
        });
        return false;
    });

    $('#btnCerrarVentana').click(function () {
        var v = document.getElementById("ventana");
        document.body.removeChild(dimmer);
        v.style.visibility = "hidden";
        $("#info").html('');
    });

});

function ventana(text) {
    var v = document.getElementById("ventana");
    dimmer = document.createElement("div");
    $('#info').append('<p>' + text + '</p>');
    dimmer.style.width = window.innerWidth + 'px';
    dimmer.style.height = window.innerHeight + 100 + 'px';
    dimmer.className = 'dimmer';
    dimmer.onclick = function () {
        document.body.removeChild(this);
        v.style.visibility = "hidden";
        $("#info").html('');
    };
    document.body.appendChild(dimmer);
    v.style.visibility = "visible";
    return false;
}

function CURPVacio() {
    valor = $('#curp').val();
    if (valor != '' && validarCURP(valor))//validar CURP
        return true;
    else { //no
        $('#resultado_curp').append('<p id="error" class="field-validation-error">Por favor introduzca el CURP del candidato a registrar.</p>');
        document.getElementById('details').style.visibility = 'hidden';
        noPermitir();
    }
    return false;
}

function permitir() {
    document.getElementById('resultado_curp').style.visibility = 'visible';
    $('input[type=text],textarea').prop('readonly', false);
    $('input[type=checkbox]').prop('disabled', false);
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