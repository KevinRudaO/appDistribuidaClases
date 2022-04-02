using appDistribuidaClases.Clases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace appDistribuidaClases.Servidor
{
    /// <summary>
    /// Descripción breve de ControladorFactura
    /// </summary>
    public class ControladorFactura : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string DatosFactura;
            StreamReader reader = new StreamReader(context.Request.InputStream);
            DatosFactura = reader.ReadToEnd();

            viewFactura vFactura = JsonConvert.DeserializeObject<viewFactura>(DatosFactura);

            clsFactura oFacturacion = new clsFactura();
            oFacturacion.oFactura = vFactura;
            switch (oFacturacion.oFactura.Comando.ToUpper())
            {
                case "GRABARFACTURA":
                    context.Response.Write(GrabarFactura(oFacturacion));
                    break;

                default:
                    context.Response.Write("Comando sin definir");
                    break;
            }
        }
        private string GrabarFactura(clsFactura oFactura)
        {
            return (oFactura.GrabarFactura());
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