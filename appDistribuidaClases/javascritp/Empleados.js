$(document).ready(function () {
    LlenarComboCargos();

   

    $("#btnConsultarTodos").click(function () {
        ConsultarTodos();
    });

    $("#btnGrabarEmpleado").click(function () {
        GrabarEmpleado();
    });
    $("#btnConsultarXDocumento").click(function () {
        ConsultarXDocumento();
    });
    $("#btnActualizar").click(function () {
        Actualizar();
    });
  
});
function ConsultarXDocumento() {
    var Documento = $("#txtDocumento").val();
    var DatosEmpleado = {
        Documento: Documento
    }
    var sURL = "http://localhost:53645/api/Empleados";
    $.ajax({
        type: "GET",
        url: sURL,
        data:DatosEmpleado,
        dataType: "json",
        success: function (respuesta) {

            $("#txtNombres").val(respuesta["NombreEmpleado"]);
            $("#txtPrimerApellido").val(respuesta["PrimerApelligo"]);
            $("#txtSegundoApellido").val(respuesta["SegundoApellido"]);
            $("#txtDireccion").val(respuesta["Direccion"]);
            $("#txtTelefono").val(respuesta["Telefono"]);
            var FechaNcto = respuesta["FechaNacimiento"].split('T')[0];
            $("#txtFechaNacimiento").val(FechaNcto);

        },
        error: function (respuesta) {
            $("#dvMensaje").html("Error: " + respuesta);
        }
    });
}

function Actualizar() {
    var Documento = $("#txtDocumento").val();
    var Nombre = $("#txtNombres").val();
    var PrimerApellido = $("#txtPrimerApellido").val();
    var SegundoApellido = $("#txtSegundoApellido").val();
    var Direccion = $("#txtDireccion").val();
    var Telefono = $("#txtTelefono").val();
    var FechaNacimiento = $("#txtFechaNacimiento").val();

    var DatosEmpleado = {
        Documento: Documento,
        NombreEmpleado: Nombre,
        PrimerApelligo: PrimerApellido,
        SegundoApellido: SegundoApellido,
        Direccion: Direccion,
        Telefono: Telefono,
        FechaNacimiento: FechaNacimiento
    }
    var sURL = "http://localhost:53645/api/Empleados";
    $.ajax({
        type: "PUT",
        url: sURL,
        contentType: "application/json",
        data: JSON.stringify(DatosEmpleado),
        dataType: "json",
        success: function (respuesta) {
            $("#dvMensaje").html(respuesta);
            ConsultarTodos();
        },
        error: function (respuesta) {
            $("#dvMensaje").html("Error: " + respuesta);
        }
    });
}



function GrabarEmpleado() {
    var Documento = $("#txtDocumento").val();
    var Nombre = $("#txtNombres").val();
    var PrimerApellido = $("#txtPrimerApellido").val();
    var SegundoApellido = $("#txtSegundoApellido").val();
    var Direccion = $("#txtDireccion").val();
    var Telefono = $("#txtTelefono").val();
    var FechaNacimiento = $("#txtFechaNacimiento").val();
    var CodigoCargo = $("#cboCargos").val();

    var DatosEmpleado = {
        Documento: Documento,
        NombreEmpleado: Nombre,
        PrimerApelligo: PrimerApellido,
        SegundoApellido: SegundoApellido,
        Direccion: Direccion,
        Telefono: Telefono,
        FechaNacimiento: FechaNacimiento,
        CodigoCargo: CodigoCargo
    }
    var sURL = "http://localhost:53645/api/Empleados";
    $.ajax({
        type: "POST",
        url: sURL,
        contentType: "application/json",
        data: JSON.stringify(DatosEmpleado),
        dataType: "json",
        success: function (respuesta) {
            $("#dvMensaje").html(respuesta);
            ConsultarTodos();
        },
        error: function (respuesta) {
            $("#dvMensaje").html("Error: " + respuesta);
        }
    });

}

function ConsultarTodos() {
    var sURL = "http://localhost:53645/api/Empleados";
    $.ajax({
        type: "GET",
        url: sURL,
        dataType: "json",
        success: function (respuesta) {
            LlenarGridDatos(respuesta, "#tblEmpleados")
        },
        error: function (respuesta) {
            $("#dvMensaje").html("Error: " + respuesta);
        }
    });

}
function LlenarComboCargos() {
    var sURL = "http://localhost:53645/api/CargoCombo";
    $.ajax({
        type: "GET",
        url: sURL,
        dataType: "json",
        success: function (respuesta) {

            LlenarComboDatos(respuesta, "#cboCargos");
            
        },
        error: function (respuesta) {
            $("#dvMensaje").html("Error: " + respuesta);
        }
    });
}

 