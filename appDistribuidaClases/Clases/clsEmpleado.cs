using libComunes.CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace appDistribuidaClases.Clases
{
    public class clsEmpleado
    {
        public List<viewComboCajeros> lstCajeros { get; set; }
        public string Error { get; set; }
        public List<viewComboCajeros> LlenarComboCajeros()
        {
            clsConexion oConexion = new clsConexion();
            oConexion.SQL = "Empleado_ComboCajeros";
            lstCajeros = new List<viewComboCajeros>();
            if (oConexion.Consultar())
            {
                if (oConexion.Reader.HasRows)
                {
                    while (oConexion.Reader.Read())
                    {
                        viewComboCajeros oCombo = new viewComboCajeros();
                        oCombo.Valor = oConexion.Reader.GetValue(0).ToString();
                        oCombo.Texto = oConexion.Reader.GetValue(1).ToString();

                        lstCajeros.Add(oCombo);
                    }
                    return lstCajeros;
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