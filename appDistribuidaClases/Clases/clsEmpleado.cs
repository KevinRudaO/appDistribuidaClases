using libComunes.CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace appDistribuidaClases.Clases
{
    public class clsEmpleado
    {
        public List<ViewComboCajeros> listCajeros { get; set; }

        public string Error { get; set; }

        public string Comando { get; set; }

        public List<ViewComboCajeros> LlenarComboCajeros()
        {
            clsConexion oConexion = new clsConexion();
            oConexion.SQL = "Empleado_Cajero";
            //oConexion.StoredProcedure=true;
            listCajeros = new List<ViewComboCajeros>();

            if(oConexion.Consultar())
            {
                if (oConexion.Reader.HasRows)
                {
                    while (oConexion.Reader.Read())
                    {
                        ViewComboCajeros oCombo = new ViewComboCajeros();
                        oCombo.Valor = oConexion.Reader.GetValue(0).ToString();
                        oCombo.Texto = oConexion.Reader.GetValue(1).ToString();
                        listCajeros.Add(oCombo);
                    }
                    return listCajeros;
                }
                else
                {
                    Error = "No hay datos";
                    return null;
                }
            }
            else
            {
                Error = oConexion.Error;
                return null;
            }

        }


    }
}