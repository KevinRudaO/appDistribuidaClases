using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using libComunes.CapaDatos;

namespace pProgramacionDistribuida.Clases
{
    public class clsCliente
    {
        #region Atributos
        public string Documento { get; set; }
        public string Nombres { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Email { get; set; }
        public string Clave { get; set; }
        public string Comando { get; set; }
        private string SQL;
        public string Error { get; private set; }
        #endregion
        #region Metodos
        public bool Insertar()
        {
            SQL = "INSERT INTO tblCliente(Documento, Nombre, PrimerApellido, SegundoApellido, " +
                  "Direccion, Telefono, FechaNacimiento, CorreoElectronico, Clave) " +
                  "VALUES (@prDocumento, @prNombre, @prPrimerApellido, @prSegundoApellido, " +
                   "@prDireccion, @prTelefono, @prFechaNacimiento, @prEmail, @prClave)";

            //Se crea el objeto de Conexión
            clsConexion oConexion = new clsConexion();
            //Se pasa el SQL que va a ejecutar
            oConexion.SQL = SQL;
            //Se pasan los parámetros
            oConexion.AgregarParametro("@prDocumento", Documento);
            oConexion.AgregarParametro("@prNombre", Nombres);
            oConexion.AgregarParametro("@prPrimerApellido", PrimerApellido);
            oConexion.AgregarParametro("@prSegundoApellido", SegundoApellido);
            oConexion.AgregarParametro("@prDireccion", Direccion);
            oConexion.AgregarParametro("@prTelefono", Telefono);
            oConexion.AgregarParametro("@prFechaNacimiento", FechaNacimiento);
            oConexion.AgregarParametro("@prEmail", Email);
            oConexion.AgregarParametro("@prClave", Clave);

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
            SQL = "SELECT       Nombre, PrimerApellido, SegundoApellido, Direccion, Telefono, " +
                                "CorreoElectronico, FechaNacimiento " +
                  "FROM         tblCliente " +
                  "WHERE        Documento=@prDocumento";

            //Se crea el objeto de Conexión
            clsConexion oConexion = new clsConexion();
            //Se pasa el SQL que va a ejecutar
            oConexion.SQL = SQL;
            //Se pasan los parámetros
            oConexion.AgregarParametro("@prDocumento", Documento);

            if (oConexion.Consultar())
            {
                if (oConexion.Reader.HasRows)
                {
                    //Tiene datos, y se deben leer
                    oConexion.Reader.Read();
                    //Se leen los datos y se asignan a las propiedades
                    Nombres = oConexion.Reader.GetString(0);
                    PrimerApellido = oConexion.Reader.GetString(1);
                    SegundoApellido = oConexion.Reader.GetString(2);
                    Direccion = oConexion.Reader.GetString(3);
                    Telefono = oConexion.Reader.GetString(4);
                    Email = oConexion.Reader.GetString(5);
                    FechaNacimiento = oConexion.Reader.GetDateTime(6);

                    return true;
                }
                else
                {
                    //No hay datos
                    Error = "No hay datos para el cliente con documento: " + Documento;
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