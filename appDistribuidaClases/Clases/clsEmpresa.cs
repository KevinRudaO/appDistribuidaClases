using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using libComunes.CapaDatos;

namespace pProgramacionDistribuida.Clases
{
    public class clsEmpresa
    {
        #region Atributos
        public  Int32 IdEmpresa { get; set; }
        public string Nit { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string SitioWEB { get; set; }

        public string Comando { get; set; }

        private string SQL;
        public string Error { get; private set; }
        #endregion
        #region Metodos
        public bool Insertar()
        {
            SQL = "Empresa_Insertar";

            //Se crea el objeto de Conexión
            clsConexion oConexion = new clsConexion();
            //Se pasa el SQL que va a ejecutar
            oConexion.SQL = SQL;
            oConexion.StoredProcedure = true;
            //Se pasan los parámetros
            oConexion.AgregarParametro("@prNit", System.Data.SqlDbType.VarChar,20,Nit);
            oConexion.AgregarParametro("@prNombre", System.Data.SqlDbType.VarChar, 200, Nombre);
            oConexion.AgregarParametro("@prEmail", System.Data.SqlDbType.VarChar, 200, Email);
            oConexion.AgregarParametro("@prDireccion", System.Data.SqlDbType.VarChar, 200, Direccion);
            oConexion.AgregarParametro("@prTelefono", System.Data.SqlDbType.VarChar, 20, Telefono);
            oConexion.AgregarParametro("@prSitioWEB", System.Data.SqlDbType.VarChar, 200, SitioWEB);

            if (oConexion.EjecutarSentencia())
            {
                return true;
            }
            else
            {
                Error = oConexion.Error;
                return false;
            }
        }
      
         public bool Actualizar()
                {
            SQL = "Empresa_Actualizar";

            //Se crea el objeto de Conexión
            clsConexion oConexion = new clsConexion();
            //Se pasa el SQL que va a ejecutar
            oConexion.SQL = SQL;
            oConexion.StoredProcedure = true;
            //Se pasan los parámetros
            oConexion.AgregarParametro("@prNit", System.Data.SqlDbType.VarChar, 20, Nit);
            oConexion.AgregarParametro("@prNombre", System.Data.SqlDbType.VarChar, 200, Nombre);
            oConexion.AgregarParametro("@prEmail", System.Data.SqlDbType.VarChar, 200, Email);
            oConexion.AgregarParametro("@prDireccion", System.Data.SqlDbType.VarChar, 200, Direccion);
            oConexion.AgregarParametro("@prTelefono", System.Data.SqlDbType.VarChar, 20, Telefono);
            oConexion.AgregarParametro("@prSitioWEB", System.Data.SqlDbType.VarChar, 200, SitioWEB);

            if (oConexion.EjecutarSentencia())
            {
                return true;
            }
            else
            {
                Error = oConexion.Error;
                return false;
            }

        }

        public bool Eliminar()
        {
            SQL = "Empresa_Eliminar";

            //Se crea el objeto de Conexión
            clsConexion oConexion = new clsConexion();
            //Se pasa el SQL que va a ejecutar
            oConexion.SQL = SQL;
            oConexion.StoredProcedure = true;
            //Se pasan los parámetros
            oConexion.AgregarParametro("@prNit", System.Data.SqlDbType.VarChar, 20, Nit);

            if (oConexion.EjecutarSentencia())
            {
                return true;
            }
            else
            {
                Error = oConexion.Error;
                return false;
            }

        }

        public bool Consultar()
        {
            SQL = "Empresa_Consultar";

            //Se crea el objeto de Conexión
            clsConexion oConexion = new clsConexion();
            //Se pasa el SQL que va a ejecutar
            oConexion.SQL = SQL;
            oConexion.StoredProcedure = true;
            //Se pasan los parámetros
            oConexion.AgregarParametro("@prNit", System.Data.SqlDbType.VarChar, 20, Nit);

            if (oConexion.Consultar())
            {
                if (oConexion.Reader.HasRows)
                {
                    oConexion.Reader.Read();
                    IdEmpresa = oConexion.Reader.GetInt32(0);
                    Nombre = oConexion.Reader.GetString(1);
                    Direccion = oConexion.Reader.GetString(2);
                    Email = oConexion.Reader.GetString(3);
                    Telefono = oConexion.Reader.GetString(4);
                    SitioWEB = oConexion.Reader.GetString(5);
                    return true;
                }
                else
                {
                    Error = "No hay datos para la empresa con NIT: " + Nit;
                    return false;
                }
            }
            else
            {
                Error = oConexion.Error;
                return false;
            }

        }

        #endregion
    }
}