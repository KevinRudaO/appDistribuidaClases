using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace appDistribuidaClases.Clases
{
    public class ViewEmpleado
    {
        public string Documento { get; set; }
        public string Nombre { get; set; }
        //** Continuan las propieades
        public string Error { get; set; }
        public string Comando { get; set; }
    }
    public class viewComboCajeros
    {
        public string Valor { get; set; }
        public string Texto { get; set; }
        public string Error { get; set; }
    }
}