using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.ExtensionMethods
{
    public static class Extensions
    {
        public static string FormatDecimal(this decimal dec, int casasDecimais)
        {
            System.Text.StringBuilder sbCasasDecimais = new System.Text.StringBuilder();
            sbCasasDecimais.Append("###########0.");
            for (int i = 0; i < casasDecimais; i++)
            {
                sbCasasDecimais.Append("0");
            }

            return dec.ToString(sbCasasDecimais.ToString());
        }

        public static string FormatDecimal(this double dec, int casasDecimais)
        {
            System.Text.StringBuilder sbCasasDecimais = new System.Text.StringBuilder();
            sbCasasDecimais.Append("###########0.");
            for (int i = 0; i < casasDecimais; i++)
            {
                sbCasasDecimais.Append("0");
            }

            return dec.ToString(sbCasasDecimais.ToString());
        }

        public static string FormatInteger(this double dec)
        {
            int temp = 0;
            int.TryParse(dec.ToString(), out temp);

            return temp.ToString();
        }

        public static Int32 ToInteger(this string value)
        {
            int temp = 0;
            int.TryParse(value.ToString(), out temp);

            return temp;
        }

        public static DateTime ToDateTime(this string value)
        {
            DateTime temp = new DateTime();
            DateTime.TryParse(value.ToString(), out temp);

            return temp;
        }

        public static double ToDouble(this string value)
        {
            double temp = 0;
            double.TryParse(value.ToString(), out temp);

            return temp;
        }

        public static decimal ToDecimal(this string value)
        {
            //double intTemp = 0;
            //double.TryParse(value, out intTemp);
            decimal intTemp = 0;
            decimal.TryParse((value.Replace(".", ",")), out intTemp);

            return intTemp;
        }

        public static Boolean ToBoolean(this string value)
        {
            if (value == null)
                return false;

            Boolean temp = false;
            Boolean.TryParse(value, out temp);

            return temp;
        }

        public static bool HasDecimal(this object dec)
        {
            decimal valorTemp = 0;
            return decimal.TryParse(dec.ToString(), out valorTemp);
        }

        public static bool IsDateTime(this string date)
        {
            DateTime valorTemp = new DateTime();
            return DateTime.TryParse(date.ToString(), out valorTemp);
        }

        public static DateTime FormatDateTime(this string date)
        {
            DateTime dateTemp;
            DateTime.TryParse(date, out dateTemp);

            return dateTemp;
        }

        public static double AjustAngulo(this double angle)
        {
            if (angle.Equals(360))
                angle = 0;

            return angle;
        }
    }
}