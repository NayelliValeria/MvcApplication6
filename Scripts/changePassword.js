$(document).ready(function () {

    $('#enviar').click(function () {
        $('.field-validation-error').remove();
        if($('#actual').val().length==0){
            $('#actual_val').append('<p class="field-validation-error">Debe introducir su contraseña acual</p>');
            document.getElementById('actual_val').style.visibility='visible';
        }
        if($('#nueva1').val()==""){
            $('#nueva1_val').append('<p class="field-validation-error">Debe introducir su contraseña acual</p>');
            document.getElementById('nueva1_val').style.visibility = 'visible';
        }
        if ($('#nueva2').val() == "") {
            $('#nueva2_val').append('<p class="field-validation-error">Debe introducir su contraseña acual</p>');
            document.getElementById('nueva2_val').style.visibility = 'visible';
        }
        if ($('#nueva1').val() != $('#nueva2').val()) {
            $('.field-validation-error').remove();
            $('#nueva1_val').append('<p class="field-validation-error">Las contraseñas no coinciden.</p>');
            $('#nueva2_val').append('<p class="field-validation-error">Las contraseñas no coinciden.</p>');
            document.getElementById('nueva1_val').style.visibility = 'visible';
            document.getElementById('nueva2_val').style.visibility = 'visible';
        } else {
            $.ajax({
                url: '/Reclutador/changePassword2',
                type: 'GET',
                data: 'pass=' + $('#actual').val() + '&pass2=' + $('#nueva1').val(),
                dataType: "text",
                success: function (mensaje) {
                    $('#resultado').append('<p class="field-validation-succes">Su nueva contraseña ha sido guardada.</p>');
                    document.getElementById('resultado').style.visibility = 'visible';
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $('#resultado').append('<p class="field-validation-error">Su nueva contraseña no se ha podido guardar, intente nuevamente.</p>');
                    document.getElementById('resultado').style.visibility = 'visible';
                }
            });
        }
    });
});