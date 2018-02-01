using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Intranet.Helpers
{
    public class Util
    {
        public static Boolean AjustToBoolean(string valor)
        {
            bool valorTemp = false;
            bool.TryParse((valor.Trim()), out valorTemp);

            return valorTemp;
        }

        public static decimal AjustToDecimal(string valor)
        {
            decimal valorTemp = 0;
            decimal.TryParse((valor.Replace(".", ",")), out valorTemp);

            return valorTemp;
        }

        public static double AjustToDouble(string valor)
        {
            double valorTemp = 0;
            double.TryParse((valor.Replace(".", ",")), out valorTemp);

            return valorTemp;
        }

        public static int AjustToInteger(string valor)
        {
            int valorTemp = 0;
            if (valor == null)
                valor = string.Empty;
            int.TryParse((valor.Replace(".", ",")), out valorTemp);

            return valorTemp;
        }
        
        public static string StringNullEmpty(string valor)
        {
            return valor == null ? string.Empty : valor.Trim();
        }

        public static Boolean ConvertToBoolean(string valor)
        {
            return valor == null ? false : true;
        }

        public static decimal ConvertMillimetersToInch(decimal valor)
        {
            valor = (valor / 25.4m);

            return valor;
        }

        public static decimal ConvertInchToMilimetro(decimal valor)
        {
            valor = (valor * 25.4m);

            return valor;
        }

        public static Intranet.Data.Enumerators.UnidadeMedida UnitMeasureReturns(FormCollection form)
        {
            Intranet.Data.Enumerators.UnidadeMedida unidadeMedida = Data.Enumerators.UnidadeMedida.MILLIMETERS;

            var rdoSistemaMedicao = form["rdoSistemaMedicao"].ToString();
            form.Remove("rdoSistemaMedicao");

            if (rdoSistemaMedicao.Equals("mm"))
            {
                unidadeMedida = Data.Enumerators.UnidadeMedida.MILLIMETERS;
            }

            if (rdoSistemaMedicao.Equals("in"))
            {
                unidadeMedida = Data.Enumerators.UnidadeMedida.INCHES;
            }

            return unidadeMedida;
        }
    }
}