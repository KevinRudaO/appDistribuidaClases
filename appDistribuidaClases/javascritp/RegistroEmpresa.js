$(document).ready(function () {
    $("#btnIngresar").click(function () {
        EjecutarAcciones("insertar");
    });
});


$(document).ready(function () {
    $("#btnActualizar").click(function () {

        EjecutarAcciones("Actualizar");
    });
});

$(document).ready(function () {
    $("#btnEliminar").click(function () {

        EjecutarAcciones("Eliminar");
    });
});


function EjecutarAcciones(Comando) {

    $("#dvMensaje").html("");

    var IdEmpresa = $("#txtIdEmpresa").val();
    var NIT = $("#txtNIT").val();
    var Nombre = $("#txtNombre").val();
    var Email = $("#txtEmail").val();
    var Direccion = $("#txtDireccion").val();
    var Telefono = $("#txtTelefono").val();
    var SitioWEB = $("#txtSitioWEB").val();

    //Revisar si la clave y la confirmación están iguales
    if (Clave !== ConfirmaClave) {
        $("#dvMensaje").html("Las claves no coinciden, por favor revise la información registrada");
        return;
    }
    //El proceso continua
    //$("#dvMensaje").html("Las claves coinciden. Grabación en progreso");
    //Crear el objeto Json con los datos del cliente
    var DatosEmpresa = {
        IdEmpresa: IdEmpresa,
        NIT: NIT,
        Nombre: Nombre,
        Email: Email,
        direccion: Direccion,
        telefono: Telefono,
        SitioWEB: SitioWEB
    };

    //Inicia el proceso de invocación de  la página del servidor con ajax
    $.ajax({
        type: "POST",
        url: "../Servidor/ControladorEmpresa.ashx",
        contentType: "application/json",
        data: JSON.stringify(DatosEmpresa),
        dataType: "html",
        success: function (RespuestaEmpresa) {
            $("#dvMensaje").html("Respuesta exitosa: " + RespuestaEmpresa);
        },
        error: function (RespuestaEmpresa) {
            $("#dvMensaje").html("Error: " + RespuestaEmpresa);
        }
    });

}


$(document).ready(function () {
    $("#btnConsultar").click(function () {
        //Inicio el mensaje en blanco
        $("#dvMensaje").html("");
        $("#dvMensaje").html("");
        var NIT = $("#txtNIT").val();

        var DatosEmpresa = {
            NIT:NIT
        };
        
       

        //Inicia el proceso de invocación de  la página del servidor con ajax
        $.ajax({
            type: "POST",
            url: "../Servidor/ControladorEmpresa.ashx",
            contentType: "application/json",
            data: JSON.stringify(DatosEmpresa),
            dataType: "json",
            success: function (RespuestaEmpresa) {
                $("#txIdEmpresa").val(RespuestaEmpresa["IdEmpresa"]);
                $("#txtNombre").val(RespuestaEmpresa["Nombre"]);
                $("#txtEmail").val(RespuestaEmpresa["Email"]);
                $("#txtDireccion").val(RespuestaEmpresa["Direccion"]);
                $("#txtTelefono").val(RespuestaEmpresa["Telefono"]);
                $("#txtSitioWEB").val(RespuestaEmpresa["SitioWEB"]);

            },
            error: function (RespuestaEmpresa) {
                $("#dvMensaje").html("Error: " + RespuestaEmpresa);
            }
        });
    });
});