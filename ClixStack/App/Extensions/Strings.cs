using System;
using System.Collections.Generic;
using System.Web;

namespace AppFramework
{
    public static class Strings
    {
        #region  | Strings |

        /// <summary>
        /// This Extention Provides a Count of Occurances of a Value within a String
        /// </summary>
        /// <param name="DataString"></param>
        /// <param name="SearchString"></param>
        /// <returns></returns>
        public static int x_PatternCount(this string DataString, string SearchString)
        {
            int Result = 0;
            Result = ((DataString.Length) - (DataString.Replace(SearchString, "").Length)) / (SearchString.Length);
            return Result;
        }

        public static int x_LastInstanceOfSubString(this string DataString, string SearchString)
        {
            return x_Position(DataString, SearchString, x_PatternCount(DataString, SearchString));
        }

        public static string x_Left(this string DataString, int NumberOfCharacters)
        {
            return DataString.Substring(0, NumberOfCharacters);
        }

        public static string x_Right(this string DataString, int NumberOfCharacters)
        {
            return DataString.Substring(DataString.Length - NumberOfCharacters, NumberOfCharacters);
        }

        public static string x_RightOfInstanceOf(this string DataString, string SearchString, int StringInstance)
        {
            if (!(x_Position(DataString, SearchString, StringInstance) == DataString.Length))
            {
                return x_Right(DataString, (DataString.Length) - (x_Position(DataString, SearchString, StringInstance) + SearchString.Length - 1));
            }
            else
            {
                return "";
            }
        }

        public static string x_LeftOfInstanceOf(this string DataString, string SearchString, int StringInstance)
        {
            if (!(x_Position(DataString, SearchString, StringInstance) == 1))
            {
                return x_Left(DataString, x_Position(DataString, SearchString, StringInstance) - 1);
            }
            else
            {
                return "";
            }
        }

        public static string x_Window(this string DataString, int StartPosition, int StopPosition)
        {
            return DataString.Substring(StartPosition, StopPosition - StartPosition);
        }

        public static int x_Position(string DataString, string SearchString, int SearchInstance)
        {
            int FoundPosition = 0;
            int CurrentInstance = 0;
            int StartPosition = 0;
            StartPosition = 0;
            FoundPosition = 0;
            CurrentInstance = 0;

            if (!(((DataString.Length) - (DataString.Replace(SearchString, "").Length)) / (SearchString.Length) == 0))
            {
                do
                {
                    FoundPosition = DataString.IndexOf(SearchString, StartPosition);
                    if (FoundPosition != -1)
                    {
                        FoundPosition = FoundPosition + 1;
                        CurrentInstance += 1;
                        StartPosition = FoundPosition;
                    }
                    else
                    {
                        return 0;
                    }
                } while (!(CurrentInstance == SearchInstance));
            }
            return FoundPosition;
        }

        public static string x_WindowReplace(this string DataString, int StartPosition, int StopPosition, string SearchString, string ReplaceString)
        {
            string LeftString = x_Left(DataString, StartPosition - 1);
            string RightString = x_Mid(DataString, StopPosition, (DataString.Length - (StopPosition)) + 1);
            string MiddleString = x_Window(DataString, StartPosition, StopPosition);
            MiddleString = MiddleString.Replace(SearchString, ReplaceString);
            return LeftString + MiddleString + RightString;
        }

        public static string WindowOfInstanceFromInstanceOf(this string DataString, string StartSearchString, int StartInstance, string EndSearchString, int InstanceFromStartString)
        {
            if (DataString.Contains(StartSearchString) & DataString.Contains(EndSearchString))
            {
                string FloatString = null;
                try
                {
                    FloatString = x_Right(DataString, (DataString.Length + 1) - x_Position(DataString, StartSearchString, StartInstance));
                    return x_Left(FloatString, x_Position(FloatString, EndSearchString, InstanceFromStartString));
                }
                catch (Exception)
                {
                    return "";
                }
            }
            return "";
        }

        /// <summary>
        /// This Extension Provides Substring Functionality with PHP or Excel Like Syntax
        /// </summary>
        /// <param name="DataString"></param>
        /// <param name="StartPosition"></param>
        /// <param name="Length"></param>
        /// <returns></returns>
        public static string x_Mid(this string DataString, int StartPosition, int Length)
        {
            return DataString.Substring(StartPosition, Length);
        }

        /// <summary>
        /// This Extention Provides a Quick GUID Generation Minus any Hyphens (-)
        /// </summary>
        /// <returns></returns>
        public static string x_NewID()
        {
            return Guid.NewGuid().ToString().ToLower().Replace("-", "");
        }

        /// <summary>
        /// This Extention Provides Fast Case Insenstive String Replacement
        /// </summary>
        /// <param name="DataString"></param>
        /// <param name="Original"></param>
        /// <param name="Replacement"></param>
        /// <returns></returns>
        public static string x_Replace(this string DataString, string Original, string Replacement)
        {
            int count, position0, position1;
            count = position0 = position1 = 0;
            string upperString = DataString.ToUpper();
            string upperPattern = Original.ToUpper();
            int inc = (DataString.Length / Original.Length) *
                      (Replacement.Length - Original.Length);
            char[] chars = new char[DataString.Length + Math.Max(0, inc)];
            while ((position1 = upperString.IndexOf(upperPattern, position0)) != -1)
            {
                for (int i = position0; i < position1; ++i)
                    chars[count++] = DataString[i];
                for (int i = 0; i < Replacement.Length; ++i)
                    chars[count++] = Replacement[i];
                position0 = position1 + Original.Length;
            }
            if (position0 == 0) return DataString;
            for (int i = position0; i < DataString.Length; ++i)
                chars[count++] = DataString[i];
            return new string(chars, 0, count);
        }

        #endregion
    }
}