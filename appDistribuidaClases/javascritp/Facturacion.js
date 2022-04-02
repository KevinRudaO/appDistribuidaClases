$(document).ready(function () {
    var oTabla = $("#tblFactura").DataTable();
    //Seleccionar la fila de la tabla
    $('#tblFactura tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
            $("#dvGrabar").show();
            $("#dvModificarEliminar").hide();
        }
        else {
            oTabla.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
            $("#dvGrabar").hide();
            $("#dvModificarEliminar").show();

            //Selecciona los datos de la fila seleccionada
            var DatosFila = oTabla.row('.selected').data();
            $("#lstTipoProducto").val(DatosFila[0]);
            ComboProducto(DatosFila[0], DatosFila[2] + '|' + DatosFila[5]);
            $("#txtCantidad").val(DatosFila[4]);
        }
    });

    const fecha = new Date();
    $("#dvFechaFactura").html(fecha.toLocaleDateString());
    //Ocultar los botones de modificar y eliminar
    $("#dvModificarEliminar").hide();

    //Invocar el método ajax para llenar el combo
    ComboCajeros();
    ComboTipoProducto();

    $("#lstTipoProducto").on('change', function () {
        var TipoProducto = $("#lstTipoProducto").val();
        ComboProducto(TipoProducto, null);
    });
    $("#lstProductos").on('change', function () {
        CalcularValorUnitario();
    });
    $("#txtCantidad").on("input", function () {
        CalcularSubtotal();
    });
    $("#btnGrabar").click(function () {
        GrabarDetalle();
    });
    $("#btnCancelar").click(function () {
        $("#dvGrabar").show();
        $("#dvModificarEliminar").hide();
    });
    $("#btnEliminar").click(function () {
        oTabla.row('.selected').remove().draw(false);
        $("#dvGrabar").show();
        $("#dvModificarEliminar").hide();
    });
    $("#btnTerminar").click(function () {
        $("#dvGrabar").show();
        $("#dvModificarEliminar").hide();
        GrabarFactura();
    });
    $("#btnLimpiar").click(function () {
        $("#dvGrabar").show();
        $("#dvModificarEliminar").hide();
        $("#dvNumeroFactura").html("");
        $("#txtDocumento").val("");
    });

    $("#btnModificar").click(function () {
        GrabarDetalle();
        oTabla.row('.selected').remove().draw(false);
        $("#dvGrabar").show();
        $("#dvModificarEliminar").hide();
    });
});
function GrabarFactura() {
    //Datos para el encabezado
    var DocumentoCliente = $("#txtDocumento").val();
    var Empleado = $("#lstEmpleados").val();
    var Comando = "GrabarFactura";
    //Para el detalle, hay que recorrer la tabla de los detalles y crear un objeto json
    var oTabla = $('#tblFactura').DataTable();
    var fieldNames = [], lstDetalle = [];
    oTabla.settings().columns()[0].forEach(function (index) {
        fieldNames.push($(oTabla.column(index).header()).text().replace(/ /g, ""));
    });
    oTabla.rows().data().toArray().forEach(function (row) {
        var item = {};
        row.forEach(function (content, index) {
            item[fieldNames[index]] = content;
        });
        lstDetalle.push(item);
    });

    //Llena en el arreglo "lstDetalle" un arreglo de tipo json con la información de la tabla
    var DatosFactura = {
        NumeroFactura: '0',
        DocumentoCliente: DocumentoCliente,
        Fecha: '1900/01/01',
        CodigoEmpleado: Empleado,
        Error: '',
        Comando: Comando,
        lstDetalle
    };
    $.ajax({
        type: "POST",
        url: "../Servidor/ControladorFactura.ashx",
        contentType: "application/json",
        data: JSON.stringify(DatosFactura),
        dataType: "html",
        success: function (respuesta) {
            $("#dvNumeroFactura").html(respuesta);
        },
        error: function (respuesta) {
            $("#dvMensaje").html("Error: " + respuesta);
        }
    });
}
function GrabarDetalle() {
    var oTabla = $("#tblFactura").DataTable();

    //Leer los datos para grabar
    var CodTipoProducto = $("#lstTipoProducto").val();
    var TipoProducto = $("#lstTipoProducto option:selected").html();
    var arrCodProducto = $("#lstProductos").val().split('|');
    var CodProducto = arrCodProducto[0];
    var Producto = $("#lstProductos option:selected").html();
    var Cantidad = $("#txtCantidad").val();
    var ValorUnitario = LeerTextoFormato($("#txtValorUnitario").val());

    oTabla.row.add([
        CodTipoProducto,
        TipoProducto,
        CodProducto,
        Producto,
        Cantidad,
        ValorUnitario
    ]).draw(false);
}
function CalcularValorUnitario() {
    var Producto = $("#lstProductos").val();
    var arrProducto = Producto.split('|');
    $("#txtValorUnitario").val(FormatoMoneda(arrProducto[1]));
    CalcularSubtotal();
}
function ComboCajeros() {
    var sURL = "../Comunes/ControladorCombos.ashx";
    var Comando = "LLENARCOMBOCAJEROS";
    var lstParametros = null;
    var ComboLlenar = "#lstEmpleados";
    LlenarComboControlador(sURL, Comando, lstParametros, ComboLlenar);
}
function ComboTipoProducto() {
    var promise = LlenarComboControlador("../Comunes/ControladorCombos.ashx", "TIPOPRODUCTO", null, "#lstTipoProducto");
    if (promise) {
        promise.then(function (value) {
            //Se invoca el llenado del combo de producto
            var TipoProducto = $("#lstTipoProducto").val();
            ComboProducto(TipoProducto, null);
        });
    }
}
function ComboProducto(TipoProducto, ProductoSeleccionado) {
    $("#lstProductos").empty();
    var sURL = "../Comunes/ControladorCombos.ashx";
    var Comando = "PRODUCTOxTIPO";
    var lstParametros = [{ "Parametro": "@prTipoProducto", "Valor": TipoProducto }];
    var ComboLlenar = "#lstProductos";
    var promise = LlenarComboControlador(sURL, Comando, lstParametros, ComboLlenar);
    if (promise) {
        promise.then(function (value) {
            if (ProductoSeleccionado !== null) {
                $("#lstProductos").val(ProductoSeleccionado);
            }
            CalcularValorUnitario();
        });
    }
}
function CalcularSubtotal() {
    var Cantidad = $("#txtCantidad").val();
    var ValorUnitario = LeerTextoFormato($("#txtValorUnitario").val());

    $("#txtSubtotal").val(FormatoMoneda(Cantidad * ValorUnitario));
}
function FormatoMoneda(Texto) {
    return accounting.formatMoney(Texto, "$", 0, ".", ",");
}
function LeerTextoFormato(Texto) {
    return accounting.unformat(Texto, ",");
}