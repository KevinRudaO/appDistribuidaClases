using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace appDistribuidaClases.Clases
{
    public class viewClienteSuper
    {
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Comando { get; set; }
        public string Error { get; set; }
        public List<viewTelefono> lstTelefono { get; set; }
    }
    public class viewTelefono
    {
        public Int32 Codigo { get; set; }
        public string Numero { get; set; }
        public Int32 CodigoTipoTelefono { get; set; }
    }
}