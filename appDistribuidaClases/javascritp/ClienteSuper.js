$(document).ready(function () {
    //INVOCA EL COMBO DE TIPO TELEFONO  
    LlenarComboTipotelefono();
    OcultarBotones();

    $("#btnAgregar").click(function () {
        GrabarTelefonos();
    });

    $("#btnCancelar").click(function () {
        OcultarBotones();
    });

    $("#btnEliminar").click(function () {
        oTabla.row('.selected').remove().draw(false);
        $("#dvGrabar").show();
        $("#dvModificarEliminar").hide();
    });

    $("#btnLimpiar").click(function () {
        $("#btnGrabarCliente").show();
        $("#btnModificar").hide();
        $("#txtDocumento").val("");
        $("#txtNombres").val("");
        $("#txtPrimerApellido").val("");
        $("#txtSegundoApellido").val("");
        $("#txtDireccion").val("");
        $("#txtFechaNacimiento").val(""); 
        $("#txtNumero").val("");
    });

    $("#btnModificar").click(function () {
        GrabarTelefonos();
        oTabla.row('.selected').remove().draw(false);
        $("#dvGrabar").show();
        $("#dvModificarEliminar").hide();
    });


    $("#btnGrabarCliente").click(function () {
        GrabarCliente();
    });

    var oTabla = $("#tblTelefonos").DataTable();
    //Seleccionar la fila de la tabla
    $('#tblTelefonos tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
            OcultarBotones();
        }
        else {
            oTabla.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
            MostrarBotones();

        }
    });


});

function GrabarCliente() {
    //Capturar datos del cliente
    var Documento = $("#txtDocumento").val();
    var Nombre = $("#txtNombres").val();
    var PrimerApellido = $("#txtPrimerApellido").val();
    var SegundoApellido = $("#txtSegundoApellido").val();
    var Direccion = $("#txtDireccion").val();
    var FechaNacimiento = $("#txtFechaNacimiento").val();
    var Comando = "GRABAR";

    //Capturar telefonos del cliente
    //Para el detalle, hay que recorrer la tabla de los detalles y crear un objeto json
    var oTabla = $('#tblTelefonos').DataTable();
    var fieldNames = [], lstTelefono = [];
    oTabla.settings().columns()[0].forEach(function (index) {
        fieldNames.push($(oTabla.column(index).header()).text().replace(/ /g, ""));
    });
    oTabla.rows().data().toArray().forEach(function (row) {
        var item = {};
        row.forEach(function (content, index) {
            item[fieldNames[index]] = content;
        });
        lstTelefono.push(item);
    });
    var DatosCliente = {
        Documento: Documento,
        Nombre: Nombre,
        PrimerApellido: PrimerApellido,
        SegundoApellido: SegundoApellido,
        Direccion: Direccion,
        FechaNacimiento: FechaNacimiento,
        Comando: Comando,
        lstTelefono
    }

    $.ajax({
        type: "POST",
        url: "../Servidor/ControladorClienteSuper.ashx",
        contentType: "application/json",
        data: JSON.stringify(DatosCliente),
        dataType: "html",
        success: function (respuesta) {
            $("#dvMensaje").html(respuesta);
        },
        error: function (respuesta) {
            $("#dvMensaje").html("Error: " + respuesta);
        }
    });

}

function OcultarBotones() {
    $("#dvGrabar").show();
    $("#dvModificarEliminar").hide();
}
function MostrarBotones() {
    $("#dvGrabar").hide();
    $("#dvModificarEliminar").show();
} 

function LlenarComboTipotelefono(){
    LlenarComboControlador("../Comunes/ControladorCombos.ashx", "TIPOTELEFONO", null, "#lstTipoTelefono");
}
function GrabarTelefonos() {
    var oTabla = $("#tblTelefonos").DataTable();

    //Leer los datos para grabar
    var CodTipoTelefono = $("#lstTipoTelefono").val();
    var TipoTelefono = $("#lstTipoTelefono option:selected").html();
    var Numero = ($("#txtNumero").val());

    oTabla.row.add([
        CodTipoTelefono,
        TipoTelefono,
        Numero 
    ]).draw(false);
}

 