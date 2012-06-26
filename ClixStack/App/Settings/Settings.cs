using System;
using System.Collections.Generic;
using System.Web;

namespace AppFramework
{
    public static class AppSettings
    {
        public static class Extensions
        {
            // Array Delimiter
            public static string[] ARRAY_DELIMITER = new String[] { "<~array~>" };
            public static string[] LIST_ITEM_DELIMITER = new String[] { "<~li~>" };
            public static string[] LIST_ITEM_COLLECTION_DELIMITER = new String[] { "<~lic~>" };
            public static string[] COMMADELIMITEDSTRING_DELIMITER = new String[] { "," };
        }

        public static class Exceptions
        {
            public const string SESSION_KEY = "EXCEPTIONWATCH";
            public const string RETURN_CODE = "return_code";
        }
    }
}

