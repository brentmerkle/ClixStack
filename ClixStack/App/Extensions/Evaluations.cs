using System;
using System.Collections.Generic;
using System.Web;

namespace AppFramework
{

    #region  | Evaluation |
    public static class Evaluations
    {

        public static bool x_IsEven(this Int64 Num)
        {
            double n = ((double)Num / (double)2);
            Int64 y = (Int64)n;
            if (n == y)
                return true;
            else
                return false;
        }

        public static bool x_IsOdd(this Int64 Num)
        {
            double n = ((double)Num / (double)2);
            Int64 y = (Int64)n;
            if (!(n == y))
                return true;
            else
                return false;
        }

        public static bool x_IsEven(this Int32 Num)
        {
            double n = ((double)Num / (double)2);
            Int64 y = (Int64)n;
            if (n == y)
                return true;
            else
                return false;
        }

        public static bool x_IsOdd(this Int32 Num)
        {
            double n = ((double)Num / (double)2);
            Int64 y = (Int64)n;
            if (!(n == y))
                return true;
            else
                return false;
        }

        public static bool x_IsNumeric(this string InputString)
        {
            try
            {
                double dob = Convert.ToDouble(InputString);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
    #endregion

}