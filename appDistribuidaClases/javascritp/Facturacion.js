$(document).ready(function () {
    const fecha = new Date();
    $("#dvFechaFactura").html(fecha.toLocaleDateString());

    //modificar los botones, modificar y eliminar
    $("#dvModificarEliminar").hide();

    ComboCajeros();
});

function ComboCajeros() {
    //Llenar combo desde el servidor

    var Comando = "LLENARCOMBOCAJEROS";
    var SURL = "../Servidor/ControladorEmpleado.ashx";
    var DatosEmpleado = {
        Comando: Comando
    }

    $.ajax({
        type: "POST",
        url: SURL,
        contentType: "application/json",
        data: JSON.stringify(DatosEmpleado),
        dataType: "json",
        success: function (Respuesta) {
            var Error = Respuesta["Error"];
            if (Error == "" || Error == null || Error == 'undefined') {
                LlenarComboEmpleados(Respuesta);
            } else {
                $("#dvMensaje").html(Error);
            }
        },
        error: function (Respuesta) {
            $("#dvMensaje").html("Error: " + Respuesta);
        }
    });

}
function LlenarComboEmpleados(Datos) {
    for (i = 0; i < Datos.length; i++) {
        $("#lstEmpleados").append('<option value=' + Datos[i].Valor + '>' + Datos[i].Texto + '</option>');
    }

}