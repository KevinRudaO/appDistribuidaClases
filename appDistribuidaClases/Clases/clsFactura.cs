using libComunes.CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace appDistribuidaClases.Clases
{
    public class clsFactura
    {
        public viewFactura oFactura { get; set; }
        private clsConexion oConexion = new clsConexion();
        public string GrabarFactura()
        {
            oConexion.AbrirTransaccion();
            if (GrabarEncabezado())
            {
                if (GrabarDetalle())
                {
                    //Aceptar la transacción -> commit
                    oConexion.AceptarTransaccion();
                    return oFactura.NumeroFactura.ToString();
                }
                else
                {
                    //Rechazar la transacción -> rollback
                    oConexion.RechazarTransaccion();
                    return "No se ejecutó la transacción " + oConexion.Error;
                }
            }
            else
            {
                //Rechazar la transacción para cerrarla
                oConexion.RechazarTransaccion();
                return "No se ejecutó la transacción" + oConexion.Error;
            }
        }
        private bool GrabarEncabezado()
        {
            //Utiliza la clase conexión
            oConexion.SQL = "Factura_GrabarEncabezado";
            oConexion.StoredProcedure = true;
            oConexion.AgregarParametro(System.Data.ParameterDirection.Input, "@prDocumento_Cliente", System.Data.SqlDbType.VarChar, 20, oFactura.DocumentoCliente);
            oConexion.AgregarParametro(System.Data.ParameterDirection.Input, "@prEmpleado", System.Data.SqlDbType.Int, 4, oFactura.CodigoEmpleado);
            oConexion.AgregarParametro(System.Data.ParameterDirection.Output, "@prNumeroFactura", System.Data.SqlDbType.Int, 4, oFactura.NumeroFactura);

            if (oConexion.EjecutarSentencia())
            {
                //Leemos el número de factura
                oFactura.NumeroFactura = Convert.ToInt32(oConexion.oCommand.Parameters["@prNumeroFactura"].Value);
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool GrabarDetalle()
        {
            //Como el detalle de la factura está en una lista, se requiere recorrerla para pasar todos los elementos al procedimiento
            foreach (viewDetalleFactura oDetalle in oFactura.lstDetalle)
            {
                //Utiliza la misma clase de conexión
                oConexion.SQL = "Factura_GrabarDetalle";
                oConexion.StoredProcedure = true;
                //Antes de iniciar el proceso de agregar parámetros se deben limpiar
                oConexion.oCommand.Parameters.Clear();
                oConexion.AgregarParametro("@prNumeroFactura", System.Data.SqlDbType.Int, 4, oFactura.NumeroFactura);
                oConexion.AgregarParametro("@prProducto", System.Data.SqlDbType.Int, 4, oDetalle.CodigoProducto);
                oConexion.AgregarParametro("@prCantidad", System.Data.SqlDbType.Int, 4, oDetalle.Cantidad);
                oConexion.AgregarParametro("@prValorUnitario", System.Data.SqlDbType.Int, 4, oDetalle.ValorUnitario);
                //si no puede ejecutar algún detalle, debe devolver false, para que el método principal devuelva la transacción
                if (!oConexion.EjecutarSentencia())
                {
                    return false;
                }
            }
            //Sólo si termina el ciclo, retorna verdadero
            return true;
        }
    }
    public class viewFactura
    {
        public Int32 NumeroFactura { get; set; }
        public string DocumentoCliente { get; set; }
        public DateTime Fecha { get; set; }
        public Int32 CodigoEmpleado { get; set; }
        public string Error { get; set; }
        public string Comando { get; set; }
        public List<viewDetalleFactura> lstDetalle { get; set; }
    }
    public class viewDetalleFactura
    {
        public Int32 Codigo { get; set; }
        public Int32 CodigoProducto { get; set; }
        public Int32 Cantidad { get; set; }
        public Int32 ValorUnitario { get; set; }
        public string Error { get; set; }
    }
}