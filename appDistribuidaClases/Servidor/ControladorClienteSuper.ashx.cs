using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using appDistribuidaClases.Clases;

namespace appDistribuidaClases.Servidor
{
    /// <summary>
    /// Descripción breve de ControladorClienteSuper
    /// </summary>
    public class ControladorClienteSuper : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string DatosClienteSuper;
            StreamReader reader = new StreamReader(context.Request.InputStream);
            DatosClienteSuper = reader.ReadToEnd();

            viewClienteSuper vClientesuper = JsonConvert.DeserializeObject<viewClienteSuper>(DatosClienteSuper);

            clsClienteSuper oClienteSuper = new clsClienteSuper();
            oClienteSuper.vClienteSuper = vClientesuper;
            switch (oClienteSuper.vClienteSuper.Comando.ToUpper())
            {
                case "GRABAR":
                    context.Response.Write(GrabarCliente(oClienteSuper));
                    break;

                default:
                    context.Response.Write("Comando sin definir");
                    break;
            }
        }
        private string GrabarCliente(clsClienteSuper oClienteSuper)
        {
            return (oClienteSuper.Grabar());
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