using libComunes.CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace appDistribuidaClases.Clases
{
    public class clsClienteSuper
    {
        public viewClienteSuper vClienteSuper {get; set;}
        private clsConexion oConexion = new clsConexion();

        public string Grabar()
        {
            oConexion.AbrirTransaccion();
            if (GrabarCliente())
            {
                if (GrabarTelefonosCliente())
                {
                    //Aceptar la transacción -> commit
                    oConexion.AceptarTransaccion();
                    return "Se grabo exitosamente al cliente: "+vClienteSuper.Nombre+" y sus telefonos";
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
        private bool GrabarCliente()
        {
            //Utiliza la clase conexión
            oConexion.SQL = "Cliente_Insertar";
            oConexion.StoredProcedure = true;
            oConexion.AgregarParametro("@prDocumento", System.Data.SqlDbType.VarChar, 20, vClienteSuper.Documento);
            oConexion.AgregarParametro("@prNombre", System.Data.SqlDbType.VarChar, 50, vClienteSuper.Nombre);
            oConexion.AgregarParametro("@prPrimerApellido", System.Data.SqlDbType.VarChar, 50, vClienteSuper.PrimerApellido);
            oConexion.AgregarParametro("@prSegundoApellido", System.Data.SqlDbType.VarChar, 50, vClienteSuper.SegundoApellido);
            oConexion.AgregarParametro("@prDireccion", System.Data.SqlDbType.VarChar, 200, vClienteSuper.Direccion);
            oConexion.AgregarParametro("@prFechaNacimiento", System.Data.SqlDbType.DateTime, 4, vClienteSuper.FechaNacimiento);

            if (oConexion.EjecutarSentencia())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool GrabarTelefonosCliente()
        {
            //Como el detalle de la factura está en una lista, se requiere recorrerla para pasar todos los elementos al procedimiento
            foreach (viewTelefono oTelefonos in vClienteSuper.lstTelefono)
            {
                //Utiliza la misma clase de conexión
                oConexion.SQL = "Telefono_Insertar";
                oConexion.StoredProcedure = true;
                //Antes de iniciar el proceso de agregar parámetros se deben limpiar
                oConexion.oCommand.Parameters.Clear();
                oConexion.AgregarParametro("@prNumero", System.Data.SqlDbType.VarChar, 20, oTelefonos.Numero);
                oConexion.AgregarParametro("@prDocumento", System.Data.SqlDbType.VarChar, 20, vClienteSuper.Documento);
                oConexion.AgregarParametro("@prTipoTelefono", System.Data.SqlDbType.Int, 4, oTelefonos.CodigoTipoTelefono);
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
}