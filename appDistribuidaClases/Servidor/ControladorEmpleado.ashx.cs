using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using appDistribuidaClases.Clases;
using Newtonsoft.Json;

namespace appDistribuidaClases.Servidor
{
    /// <summary>
    /// Descripción breve de Controlador_Empresa
    /// </summary>
    public class ControladorEmpleado : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string DatosEmpleado;
            StreamReader reader = new StreamReader(context.Request.InputStream);
            DatosEmpleado = reader.ReadToEnd();

            clsEmpleado oEmpleado = JsonConvert.DeserializeObject<clsEmpleado>(DatosEmpleado);

            switch (oEmpleado.Comando.ToUpper())
            {
                case "INSERTAR":
                    context.Response.Write("Comando sin definir");
                    break;
                case "ACTUALIZAR":
                    context.Response.Write("Comando sin definir");
                    break;
                case "ELIMINAR":
                    context.Response.Write("Comando sin definir");
                    break;
                case "CONSULTAR":
                    context.Response.Write("Comando sin definir");
                    break;
                case "LLENARCOMBOCAJEROS":
                    context.Response.Write(JsonConvert.SerializeObject(LlenarComboCajeros()));
                    break;
                default:
                    context.Response.Write("Comando sin definir");
                    break;
            }
        }

        private List<ViewComboCajeros> LlenarComboCajeros()
        {
            clsEmpleado oEmpleado = new clsEmpleado();
            return oEmpleado.LlenarComboCajeros();
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
