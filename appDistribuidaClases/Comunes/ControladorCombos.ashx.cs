using libComunes.CapaObjetos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace appDistribuidaClases.Comunes
{
    /// <summary>
    /// Descripción breve de ControladorCombos
    /// </summary>
    public class ControladorCombos : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string DatosCombo;
            StreamReader reader = new StreamReader(context.Request.InputStream);
            DatosCombo = reader.ReadToEnd();

            viewCombo vCombo = JsonConvert.DeserializeObject<viewCombo>(DatosCombo);
            string Respuesta;

            switch (vCombo.Comando.ToUpper())
            {
                case "LLENARCOMBOCAJEROS":
                    Respuesta = LlenarCombo(vCombo, "Empleado_ComboCajeros");
                    break;
                case "TIPOPRODUCTO":
                    Respuesta = LlenarCombo(vCombo, "TipoProducto_LlenarCombo");
                    break;
                case "PRODUCTOXTIPO":
                    Respuesta = LlenarCombo(vCombo, "Producto_LlenarComboXTipo");
                    break;
                case "TIPOTELEFONO":
                    Respuesta = LlenarCombo(vCombo, "TipoTelefono_LlenarCombo");
                    break;
                default:
                    Respuesta = "Comando sin definir";
                    break;
            }

            context.Response.Write(Respuesta);
        }
        private string LlenarCombo(viewCombo vCombo, string SQL)
        {
            vCombo.SQL = SQL;
            clsComboListas oCombo = new clsComboListas();
            oCombo.vCombo = vCombo;
            return JsonConvert.SerializeObject(oCombo.ListarCombos());
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