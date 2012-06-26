using System;
using System.Collections.Generic;
using System.Web;

namespace AppFramework
{

    #region | Conversion |

    public static class Conversion
    {

        public static Byte x_ToByte(this string Value)
        {
            try
            {
                return Byte.Parse(Value);
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
        }

        public static Guid x_ToGuid(this string Value)
        {
            try
            {
                return new Guid(Value);
            }
            catch (Exception ex)
            {
                return Guid.Empty;
                throw ex;
            }
        }

        public static DateTime x_ToDateTime(this string Value)
        {
            try
            {
                return DateTime.Parse(Value);
            }
            catch (Exception ex)
            {
                return DateTime.Now;
                throw ex;
            }
        }

        public static int x_ToInt(this string Value)
        {
            try
            {
                return Int32.Parse(Value);
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
        }

        public static double x_ToDouble(this string Value)
        {
            try
            {
                return double.Parse(Value);
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
        }

        public static string x_ToSqlBit(this bool Value)
        {
            if (Value != null)
            {
                try
                {
                    if (Value)
                    {
                        return "1";
                    }
                    else
                    {
                        return "0";
                    }

                }
                catch (Exception)
                {

                    return "0";
                }
            }
            else
            {
                return "0";
            }
        }

        public static string x_ToString(this bool Value)
        {
            if (Value != null)
            {
                try
                {
                    if (Value)
                    {
                        return "true";
                    }
                    else
                    {
                        return "false";
                    }

                }
                catch (Exception)
                {

                    return "false";
                }
            }
            else
            {
                return "false";
            }
        }

        public static bool x_ToBool(this string Value)
        {
            try
            {
                Value = Value.ToLower().Replace("true", "1");
                Value = Value.ToLower().Replace("false", "0");
                return Convert.ToBoolean(Int32.Parse(Value));
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        public static bool x_ToBool(this int Value)
        {
            try
            {
                return Convert.ToBoolean(Value);
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        public static string x_ToYesNo(this bool Value)
        {
            try
            {
                return Value == true ? "Yes" : "No";
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public static string x_ToSqlSortDirection(this System.Web.UI.WebControls.SortDirection sortDirection)
        //{
        //    string newSortDirection = String.Empty;

        //    switch (sortDirection)
        //    {
        //        case System.Web.UI.WebControls.SortDirection.Ascending:
        //            newSortDirection = "ASC";
        //            break;

        //        case System.Web.UI.WebControls.SortDirection.Descending:
        //            newSortDirection = "DESC";
        //            break;
        //    }

        //    return newSortDirection;
        //}

    }
    #endregion

}
