using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Collections;

namespace AppFramework
{

    #region  | Collections |
    public static class Collections
    {

        public static bool x_ContainsValue(this string[] ArrayToCheck, string StringToLookFor)
        {
            foreach (string Line in ArrayToCheck)
            {
                if (Line.Contains(StringToLookFor))
                    return true;

            }
            return false;
        }
        public static bool x_HasValue(this string[] ArrayToSearch, string SearchValue)
        {
            foreach (string Line in ArrayToSearch)
            {
                if (Line == SearchValue)
                    return true;
            }
            return false;
        }

        public static int x_GetValueIndex(this string[] ArrayToSearch, string SearchValue)
        {
            int current = 0;
            foreach (string Line in ArrayToSearch)
            {
                if (Line == SearchValue)
                    break;
                current += 1;
            }
            return current;
        }

        //public static ListItemCollection x_StingToListItemCollection(this string LICString)
        //{
        //    ListItemCollection lic = new ListItemCollection();
        //    ListItem li;
        //    string[] lia;
        //    string[] lics = LICString.Split(AppSettings.Extensions.LIST_ITEM_COLLECTION_DELIMITER, StringSplitOptions.None);
        //    foreach (string lis in lics)
        //    {
        //        li = new ListItem();
        //        lia = lis.Split(AppSettings.Extensions.LIST_ITEM_DELIMITER, StringSplitOptions.None);
        //        li.Text = lia[0];
        //        li.Value = lia[1];
        //        lic.Add(li);
        //    }
        //    return lic;
        //}

        //public static string x_ListItemCollectionToString(this ListItemCollection LIC)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    foreach (ListItem li in LIC)
        //    {
        //        if (sb.Length > 0)
        //        {
        //            sb.Append(AppSettings.Extensions.LIST_ITEM_COLLECTION_DELIMITER[0]);
        //        }
        //        sb.Append(li.Text);
        //        sb.Append(AppSettings.Extensions.LIST_ITEM_DELIMITER[0]);
        //        sb.Append(li.Value);
        //    }
        //    return sb.ToString();
        //}

        public static string[] x_RemoveEmptyValues(this string[] ArrayToTrim, bool TrimElements)
        {
            string[] OutputArray = null;
            for (int i = 0; i <= ArrayToTrim.Length - 1; i++)
            {
                if (!string.IsNullOrEmpty(ArrayToTrim[i].Trim()))
                {
                    if ((OutputArray == null))
                    {
                        OutputArray = new string[1];
                    }
                    else
                    {
                        Array.Resize(ref OutputArray, OutputArray.Length + 1);
                    }
                    OutputArray[OutputArray.Length - 1] = (TrimElements ? ArrayToTrim[i].Trim() : ArrayToTrim[i]);
                }
            }
            return OutputArray;
        }

        public static Array x_TrimValues(this string[] ArrayToTrim)
        {
            for (int i = 0; i <= ArrayToTrim.Length - 1; i++)
            {
                ArrayToTrim[i] = ArrayToTrim[i].Trim();
            }
            return ArrayToTrim;
        }

        public static string x_AddToCommaDelimitedString(this string SourceStringArray, string AddString)
        {
            string resultStringArray = SourceStringArray;
            if(String.IsNullOrEmpty(SourceStringArray))
            {
                resultStringArray = AddString;
            }
            else
            {
                resultStringArray += ", " + AddString;
            }
            return resultStringArray;
        }

        public static string x_ToDelimitedString(this string[] SourceStringArray)
        {
            StringBuilder OutString = null;
            int count = SourceStringArray.Length - 1;
            int current = 0;

            foreach (object Item in SourceStringArray)
            {
                OutString.Append(Item);
                if (current < count)
                {
                    OutString.Append(AppSettings.Extensions.ARRAY_DELIMITER[0]);
                }
                current += 1;
            }
            return OutString.ToString();
        }

        public static string x_ToCommaDelimitedString(this int[] SourceIntArray)
        {
            StringBuilder OutString = null;
            int count = SourceIntArray.Length - 1;
            int current = 0;

            foreach (int Item in SourceIntArray)
            {
                OutString.Append(Item.ToString());
                if (current < count)
                {
                    OutString.Append(AppSettings.Extensions.COMMADELIMITEDSTRING_DELIMITER[0]);
                }
                current += 1;
            }
            return OutString.ToString();
        }

        public static string x_ToCommaDelimitedString(this string[] SourceStringArray)
        {
            StringBuilder OutString = null;
            int count = SourceStringArray.Length - 1;
            int current = 0;

            foreach (string Item in SourceStringArray)
            {
                OutString.Append(Item);
                if (current < count)
                {
                    OutString.Append(AppSettings.Extensions.COMMADELIMITEDSTRING_DELIMITER[0]);
                }
                current += 1;
            }
            return OutString.ToString();
        }

        public static string[] x_CommaDelimitedStringToStringArray(this string CommaDelimitedString)
        {
            return CommaDelimitedString.Split(AppSettings.Extensions.COMMADELIMITEDSTRING_DELIMITER, StringSplitOptions.None);
        }

        public static int[] x_CommaDelimitedStringToIntArray(this string CommaDelimitedString)
        {
            return CommaDelimitedString.Split(AppSettings.Extensions.COMMADELIMITEDSTRING_DELIMITER, StringSplitOptions.None).x_StringArrayToIntArray();
        }

        public static string[] x_AddValue(string[] ArrayToAdd, string ValueToAdd)
        {
            if (ArrayToAdd == null)
            {
                ArrayToAdd = new string[1];
            }
            else
            {
                Array.Resize(ref ArrayToAdd, ArrayToAdd.Length + 1);
            }
            ArrayToAdd[ArrayToAdd.Length - 1] = ValueToAdd;
            return ArrayToAdd;
        }

        public static string[] x_RemoveIndex(this string[] SourceStringArray, int IndexToRemove)
        {
            // TODO
            return SourceStringArray;
        }

        public static string x_ToDelimitedString(this ArrayList SourceArrayList)
        {
            StringBuilder OutString = null;
            for (int i = 0; i < SourceArrayList.Count; i++)
            {
                OutString.Append(SourceArrayList[i].ToString());
                if (i < SourceArrayList.Count - 1)
                {
                    OutString.Append(AppSettings.Extensions.ARRAY_DELIMITER[0]);
                }
            }
            return OutString.ToString();
        }

        public static int[] x_StringArrayToIntArray(this string[] SourceStringArray)
        {
            int[] iarray = null;
            for (int i = 0; i < SourceStringArray.Length; i++)
            {
                if ((iarray == null))
                {
                    iarray = new int[1];
                }
                else
                {
                    Array.Resize(ref iarray, iarray.Length + 1);
                }
                iarray[i] = SourceStringArray[i].x_ToInt();
            }
            return iarray;
        }

        public static string x_ToCommaDelimitedString(this ArrayList SourceArrayList)
        {
            StringBuilder OutString = new StringBuilder();
            for (int i = 0; i < SourceArrayList.Count; i++)
            {
                OutString.Append(SourceArrayList[i].ToString());
                if (i < SourceArrayList.Count - 1)
                {
                    OutString.Append(AppSettings.Extensions.COMMADELIMITEDSTRING_DELIMITER[0]);
                }
            }
            return OutString.ToString();
        }

        public static ArrayList x_DelimitedStringToArrayList(this string SourceString)
        {
            return new ArrayList(SourceString.Split(AppSettings.Extensions.ARRAY_DELIMITER, StringSplitOptions.None));
        }

        public static string[] x_DelimitedStringToStringArray(this string SourceString)
        {
            return SourceString.Split(AppSettings.Extensions.ARRAY_DELIMITER, StringSplitOptions.None);
        }

        public static ArrayList x_CommaDelimitedStringToArrayList(this string SourceString)
        {
            return new ArrayList(SourceString.Split(AppSettings.Extensions.COMMADELIMITEDSTRING_DELIMITER, StringSplitOptions.None));
        }

        public static void x_AddValue(this ArrayList SourceArrayList, string ValueToAdd)
        {
            SourceArrayList.Add(ValueToAdd);
        }

        public static void x_RemoveValue(this ArrayList SourceArrayList, string ValueToRemove)
        {
            SourceArrayList.RemoveAt(SourceArrayList.IndexOf(ValueToRemove));
        }

        public static int[] x_ToIntArray(this ArrayList SourceArrayList)
        {
            int[] iarray = null;
            for (int i = 0; i < SourceArrayList.Count; i++)
            {
                if ((iarray == null))
                {
                    iarray = new int[1];
                }
                else
                {
                    Array.Resize(ref iarray, iarray.Length + 1);
                }
                iarray[i] = SourceArrayList[i].ToString().x_ToInt();
            }
            return iarray;
        }

    }

    public class Collection
    {
        private ArrayList _MyCollection = new ArrayList();

        public ArrayList MyCollection
        {
            get { return _MyCollection; }
            set { _MyCollection = value; }
        }

        public void SortAscending()
        {
            MyCollection.Sort();
        }

        public void SortDescending()
        {
            MyCollection.Reverse();
        }

        public void AddItem(object ItemToAdd)
        {
            MyCollection.Add(ItemToAdd);
        }

        public void RemoveItem(object ItemToRemove)
        {
            MyCollection.Remove(ItemToRemove);
        }

        public bool HasItem(object ItemToCheckFor)
        {
            return MyCollection.IndexOf(ItemToCheckFor) == -1 ? false : true;
        }

    }
    #endregion

}