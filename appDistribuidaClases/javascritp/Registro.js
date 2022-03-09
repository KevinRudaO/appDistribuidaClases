$(document).ready(function () {
    ListarClientes();
});
function ListarClientes() {
    var DatosCliente = {
        fechanacimiento: "1900/02/01",
        comando: "Llenar_tabla"
    };

    //Inicia el proceso de invocación de  la página del servidor con ajax
    $.ajax({
        type: "POST",
        url: "../Servidor/RegistroCliente.ashx",
        contentType: "application/json",
        data: JSON.stringify(DatosCliente),
        dataType: "json",
        success: function (RespuestaCliente) {
            LlenarTabla(RespuestaCliente);
        },
        error: function (RespuestaCliente) {
            $("#dvMensaje").html("Error: " + RespuestaCliente);
        }
    });
}

function LlenarTabla(DatosCliente) {
    $("#tblClientes").DataTable({
        data: DatosCliente,
        columns: [
            { data: 'Documento', title: ' DOCUMENTO' },
            { data: 'Nombres', title: ' Nombres' },
            { data: 'PrimerApellido', title: ' Primer Apellido' },
            { data: 'SegundoApellido', title: ' Segundo Apellido' },
            { data: 'Direccion', title: ' Direccion' },
            { data: 'Telefono', title: ' Telefono' },
            { data: 'FechaNacimiento', title: ' Fecha de Nacimiento' },
            { data: 'Email', title: ' Email' }

        ],
        destroy: true
    });
}


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


function EjecutarAcciones(Comando){

    $("#dvMensaje").html("");

    var Documento = $("#txtDocumento").val();
    var Nombre = $("#txtNombres").val();
    var PrimerApellido = $("#txtPrimerApellido").val();
    var SegundoApellido = $("#txtSegundoApellido").val();
    var FechaNacimiento = $("#txtFechaNacimiento").val();
    var Direccion = $("#txtDireccion").val();
    var Telefono = $("#txtTelefono").val();
    var CorreoElectronico = $("#txtCorreo").val();
    var Clave = $("#txtClave").val();
    var ConfirmaClave = $("#txtConfirmacionClave").val();
    var Comando = Comando;

    //Revisar si la clave y la confirmación están iguales
    if (Clave !== ConfirmaClave) {
        $("#dvMensaje").html("Las claves no coinciden, por favor revise la información registrada");
        return;
    }
    //El proceso continua
    //$("#dvMensaje").html("Las claves coinciden. Grabación en progreso");
    //Crear el objeto Json con los datos del cliente
    var DatosCliente = {
        documento: Documento,
        nombres: Nombre,
        primerapellido: PrimerApellido,
        segundoapellido: SegundoApellido,
        direccion: Direccion,
        telefono: Telefono,
        fechanacimiento: FechaNacimiento,
        email: CorreoElectronico,
        clave: Clave,
        comando: Comando
    };

    //Inicia el proceso de invocación de  la página del servidor con ajax
    $.ajax({
        type: "POST",
        url: "../Servidor/RegistroCliente.ashx",
        contentType: "application/json",
        data: JSON.stringify(DatosCliente),
        dataType: "html",
        success: function (RespuestaCliente) {
            $("#dvMensaje").html("Respuesta exitosa: " + RespuestaCliente);
        },
        error: function (RespuestaCliente) {
            $("#dvMensaje").html("Error: " + RespuestaCliente);
        }
    });

}


$(document).ready(function () {
    $("#btnConsultar").click(function () {
        //Inicio el mensaje en blanco
        $("#dvMensaje").html("");
        $("#dvMensaje").html("");

        var Documento = $("#txtDocumento").val();
        var Nombre = $("#txtNombres").val();
        var PrimerApellido = $("#txtPrimerApellido").val();
        var SegundoApellido = $("#txtSegundoApellido").val();
        var FechaNacimiento = $("#txtFechaNacimiento").val();
        FechaNacimiento === '' ? FechaNacimiento = "1900/01/01" : FechaNacimiento = FechaNacimiento;

        var Direccion = $("#txtDireccion").val();
        var Telefono = $("#txtTelefono").val();
        var CorreoElectronico = $("#txtCorreo").val();
        var Clave = $("#txtClave").val();
        var ConfirmaClave = $("#txtConfirmacionClave").val();
        var Comando = "Consultar";

        //Revisar si la clave y la confirmación están iguales
        if (Clave !== ConfirmaClave) {
            $("#dvMensaje").html("Las claves no coinciden, por favor revise la información registrada");
            return;
        }
        //El proceso continua
        //$("#dvMensaje").html("Las claves coinciden. Grabación en progreso");
        //Crear el objeto Json con los datos del cliente
        var DatosCliente = {
            documento: Documento,
            nombres: Nombre,
            primerapellido: PrimerApellido,
            segundoapellido: SegundoApellido,
            direccion: Direccion,
            telefono: Telefono,
            fechanacimiento: FechaNacimiento,
            email: CorreoElectronico,
            clave: Clave,
            comando: Comando
        };

        //Inicia el proceso de invocación de  la página del servidor con ajax
        $.ajax({
            type: "POST",
            url: "../Servidor/RegistroCliente.ashx",
            contentType: "application/json",
            data: JSON.stringify(DatosCliente),
            dataType: "json",
            success: function (RespuestaCliente) {
                $("#txtNombres").val(RespuestaCliente["Nombres"]);
                $("#txtPrimerApellido").val(RespuestaCliente["PrimerApellido"]);
                $("#txtSegundoApellido").val(RespuestaCliente["SegundoApellido"]);
                $("#txtDireccion").val(RespuestaCliente["Direccion"]);
                $("#txtTelefono").val(RespuestaCliente["Telefono"]);
                var FechaNcto = RespuestaCliente["FechaNacimiento"].split('T')[0];
                $("#txtFechaNacimiento").val(FechaNcto);
                $("#txtCorreo").val(RespuestaCliente["Email"]);

            },
            error: function (RespuestaCliente) {
                $("#dvMensaje").html("Error: " + RespuestaCliente);
            }
        });
    });
});