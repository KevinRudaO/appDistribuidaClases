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
            string DatosEmpresa;
            StreamReader reader = new StreamReader(context.Request.InputStream);
            DatosEmpresa = reader.ReadToEnd();

            clsEmpresa oEmpresa = JsonConvert.DeserializeObject<clsEmpresa>(DatosEmpresa);

            switch (oEmpresa.Comando.ToUpper())
            {
                case "INSERTAR":
                    context.Response.Write("Sin definir");
                    break;
                case "ACTUALIZAR":
                    context.Response.Write("Sin definir");
                    break;
                case "ELIMINAR":
                    context.Response.Write("Sin definir");
                    break;
                case "CONSULTAR":
                    context.Response.Write("Sin definir");
                    break;
                case "LLENARCOMBOCAJEROS":
                    context.Response.Write(JsonConvert.SerializeObject(LlenarComboCajeros()));
                    break;
                default:
                    context.Response.Write("Comando sin definir");
                    break;
            }
        }
        private List<viewComboCajeros> LlenarComboCajeros()
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
