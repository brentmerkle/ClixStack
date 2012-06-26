using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using System.Xml;

namespace AppFramework
{
    public static class Objects
    {

        #region | Objects |

        public static void x_SetProperty(this object obj, string PropertyName, object Value)
        {
            PropertyInfo propertyInfo = obj.GetType().GetProperty(PropertyName);
            propertyInfo.SetValue(obj, Convert.ChangeType(Value, propertyInfo.PropertyType), null);
        }

        public static object x_GetPropertyValue(this object obj, string PropertyName)
        {
            foreach (string part in PropertyName.Split('.'))
            {
                if (obj == null) { return null; }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj, null);
            }
            return obj;
        }

        public static string x_GetPropertyValueAsString(this object obj, string PropertyName)
        {
            foreach (string part in PropertyName.Split('.'))
            {
                if (obj == null) { return null; }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj, null);
            }
            return obj.ToString();
        }

        public static bool x_GetPropertyValueAsBool(this object obj, string PropertyName)
        {
            foreach (string part in PropertyName.Split('.'))
            {
                if (obj == null) { return false; }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null) { return false; }

                obj = info.GetValue(obj, null);
            }
            return obj.ToString().x_ToBool();
        }

        public static DataTable x_ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            if (data != null)
            {
                for (int i = 0; i < props.Count; i++)
                {
                    PropertyDescriptor prop = props[i];
                    table.Columns.Add(prop.Name, prop.PropertyType);
                }
                object[] values = new object[props.Count];
                foreach (T item in data)
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = props[i].GetValue(item);
                    }
                    table.Rows.Add(values);
                }
                return table;
            }
            else
            {
                return null;
            }
        }

        public static string x_SerializeAsXML(this object obj)
        {
            //Check is object is serializable before trying to serialize
            if (obj.GetType().IsSerializable)
            {
                using (var stream = new MemoryStream())
                {
                    var serializer = new XmlSerializer(obj.GetType());
                    serializer.Serialize(stream, obj);
                    var bytes = new byte[stream.Length];
                    stream.Position = 0;
                    stream.Read(bytes, 0, bytes.Length);
                    return Encoding.UTF8.GetString(bytes);
                }
            }
            throw new NotSupportedException(string.Format("{0} is not serializable.", obj.GetType()));
        }

        public static T x_DeserializeXML<T>(this string serializedData)
        {
            if (String.IsNullOrEmpty(serializedData)) { return default(T); }
            var serializer = new XmlSerializer(typeof(T));
            var reader = new XmlTextReader(new StringReader(serializedData));
            return (T)serializer.Deserialize(reader);
        }

        public static string x_SerializeAsBytes(this object obj)
        {
            if (obj == null) { return ""; }
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return Bytes_To_String(ms.ToArray());
        }

        public static T x_DeserializeBytes<T>(this string str)
        {
            if (String.IsNullOrEmpty(str)) { return default(T); }
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            byte[] arrBytes = String_To_Bytes(str);// Decoding
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            return (T)binForm.Deserialize(memStream); ;
        }


        private static byte[] String_To_Bytes(string strInput)
        {
            // allocate byte array based on half of string length
            int numBytes = (strInput.Length) / 2;
            byte[] bytes = new byte[numBytes];

            // loop through the string - 2 bytes at a time converting it to decimal equivalent and store in byte array
            // x variable used to hold byte array element position
            for (int x = 0; x < numBytes; ++x)
            {
                bytes[x] = Convert.ToByte(strInput.Substring(x * 2, 2), 16);
            }

            // return the finished byte array of decimal values
            return bytes;
        }

        // convert the byte array back to a true string
        private static string Bytes_To_String(byte[] bytes_Input)
        {
            StringBuilder strTemp = new StringBuilder(bytes_Input.Length * 2);
            foreach (byte b in bytes_Input)
            {
                strTemp.Append(b.ToString("X02"));
            }
            return strTemp.ToString();
        }

        #endregion
    }
}