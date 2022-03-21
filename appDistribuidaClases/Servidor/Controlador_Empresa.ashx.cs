using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using appDistribuidaClases.Clases;

namespace appDistribuidaClases.Servidor
{
    /// <summary>
    /// Descripción breve de Controlador_Empresa
    /// </summary>
    public class Controlador_Empresa : IHttpHandler
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
                    context.Response.Write(InsertarEmpresa(oEmpresa));
                    break;
                case "ACTUALIZAR":
                    context.Response.Write(ActualizarEmpresa(oEmpresa));
                    break;
                case "ELIMINAR":
                    context.Response.Write(EliminarEmpresa(oEmpresa));
                    break;
                case "CONSULTAR":
                    context.Response.Write(JsonConvert.SerializeObject(Consultar(oEmpresa)));
                    break;
                default:
                    context.Response.Write("Comando sin definir");
                    break;
            }
        }
        private clsEmpresa Consultar(clsEmpresa oEmpresa)
        {
            oEmpresa.Consultar();
            
            return oEmpresa;
        }
        private string InsertarEmpresa(clsEmpresa oEmpresa)
        {
            //string Respuesta = Validar(oEmpresa);
            if (oEmpresa.Insertar())
            {
                return "Registro ingresado con éxito";
            }
            else
            {
                return oEmpresa.Error;
            }
        }
        private string ActualizarEmpresa(clsEmpresa oEmpresa)
        {
            //string Respuesta = Validar(oEmpresa);
            if (oEmpresa.Actualizar())
            {
                return "Registro actualizado con éxito";
            }
            else
            {
                return oEmpresa.Error;
            }
        }
        private string EliminarEmpresa(clsEmpresa oEmpresa)
        {
            //string Respuesta = Validar(oEmpresa);
            if (oEmpresa.Eliminar())
            {
                return "Registro eliminado con éxito";
            }
            else
            {
                return oEmpresa.Error;
            }
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
