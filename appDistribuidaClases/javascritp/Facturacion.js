$(document).ready(function () {
    const fecha = new Date();
    $("#dvFechaFactura").html(fecha.toLocaleDateString());

    //Ocultar los botones, modificar y eliminar
    $("#dvModificarEliminar").hide();

    ComboCajeros();
    ComboTipoProducto();

    $("#lstTipoProducto").on('change', function () {
        var TipoProducto = $("#lstTipoProducto").val();
        ComboProducto(TipoProducto)
    })
    $("#lstProductos").on('change', function () {
        CalcularValorUnitario();
    })
});

function ComboCajeros() {
   
    var SURL = "../Comunes/ControladorCombos.ashx";
    var Comando = "LLENARCOMBOCAJEROS";
    var lstParametros = null;
    var ComboLlenar = "#lstEmpleados";
    LlenarComboControlador(SURL, Comando, lstParametros, ComboLlenar)

}
function ComboTipoProducto() {

    var SURL = "../Comunes/ControladorCombos.ashx";
    var Comando = "TIPOPRODUCTO";
    var lstParametros = null;
    var ComboLlenar = "#lstTipoProducto";
    var promise = LlenarComboControlador(SURL, Comando, lstParametros, ComboLlenar)
    if (promise) {
        promise.then(function (value) {
            var TipoProducto = $("#lstTipoProducto").val();
            ComboProducto(TipoProducto);
        });
    }
}

function ComboProducto(TipoProducto) {
    $("#lstProductos").empty();
    var SURL = "../Comunes/ControladorCombos.ashx";
    var Comando = "PRODUCTO";
    var lstParametros = [{ "Parametro": "@prTipoPoducto", "Valor": TipoProducto }];
    var ComboLlenar = "#lstProductos";
    var promise = LlenarComboControlador(SURL, Comando, lstParametros, ComboLlenar)
    if (promise) {
        promise.then(function (value) {
            CalcularValorUnitario();
        });
    }
}

function CalcularValorUnitario() {
    var Producto = $("#lstProductos").val();
    var arrProducto = Producto.split('|');
    $("#txtValorUnitario").val(arrProducto[1]);
}
