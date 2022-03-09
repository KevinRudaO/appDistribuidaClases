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
           
            switch (oCliente.Comando.ToUpper())
            {
                case "INSERTAR":
                    context.Response.Write(InsertarCliente(oCliente));
                    break;
                case "CONSULTAR":
                    context.Response.Write(JsonConvert.SerializeObject(Consultar(oCliente)));
                    break;
                case "ACTUALIZAR":
                    context.Response.Write(ActualizarCliente(oCliente));
                    break;
                case "ELIMINAR":
                    context.Response.Write(EliminarCliente(oCliente));
                    break;
                case "LLENAR_TABLA":
                    context.Response.Write(LlenarTabla());
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

        private string ActualizarCliente(clsCliente oCliente)
        {
            //string Respuesta = Validar(oCliente);
            if (oCliente.Acualizar())
            {
                return "Registro actualizado con éxito";
            }
            else
            {
                return oCliente.Error;
            }
        }

        private string EliminarCliente(clsCliente oCliente)
        {
            //string Respuesta = Validar(oCliente);
            if (oCliente.Eliminar())
            {
                return "Registro eliminado con éxito";
            }
            else
            {
                return oCliente.Error;
            }
        }

        private string LlenarTabla()
        {
            clsCliente oCliente = new clsCliente();
            return JsonConvert.SerializeObject(oCliente.LlenarTabla());
          
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