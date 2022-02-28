using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Newtonsoft.Json;
using pProgramacionDistribuida.Clases;

namespace pProgramacionDistribuida.Servidor
{
    /// <summary>
    /// Descripción breve de RegistroCliente
    /// </summary>
    public class RegistroCliente : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string DatosCliente;
            StreamReader reader = new StreamReader(context.Request.InputStream);
            DatosCliente = reader.ReadToEnd();
            
            clsCliente oCliente = JsonConvert.DeserializeObject<clsCliente>(DatosCliente);
            /*
            context.Response.Write("Documento: " + oCliente.Documento + ", " + "Nombres: " + 
                oCliente.Nombres + ", Apellidos: " + oCliente.PrimerApellido + " " + oCliente.SegundoApellido +
                ", Dirección y teléfono: "+ oCliente.Direccion + " - " + oCliente.Telefono + ", " +
                "Fecha Nacimiento: " + oCliente.FechaNacimiento.ToString("yyyy-MMM-dd") + ", Email: " + oCliente.Email + ", " +
                "Clave: " + oCliente.Clave);
            if (oCliente.Insertar())
            {
                context.Response.Write("Registro ingresado con éxito");
            }
            else
            {
                context.Response.Write(oCliente.Error);
            }
            */
            switch (oCliente.Comando.ToUpper())
            {
                case "INSERTAR":
                    context.Response.Write(InsertarCliente(oCliente));
                    break;
                case "CONSULTAR":
                    context.Response.Write(JsonConvert.SerializeObject(Consultar(oCliente)));
                    break;
                default:
                    context.Response.Write("Comando sin definir");
                    break;
            }
        }
        private clsCliente Consultar(clsCliente oCliente)
        {
            oCliente.Consultar();
            return oCliente;
        }
        private string InsertarCliente(clsCliente oCliente)
        {
            //string Respuesta = Validar(oCliente);
            if (oCliente.Insertar())
            {
                 return "Registro ingresado con éxito";
            }
            else
            {
                return oCliente.Error;
            }
        }
        private string Validar(clsCliente oCliente)
        {
            if (string.IsNullOrEmpty(oCliente.Nombres))
            {
                return "No definió el nombre del cliente";
            }
            return "";
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}