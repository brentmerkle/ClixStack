using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace ClixStack
{
    public static class ExceptionManager
    {
        public class NewException : Exception, ISerializable
        {

            public NewException(string message)
                : base(message)
            {
                // Add implementation.
            }

        }
        public static Exception HandleException(this Exception RootException, bool Trace)
        {
            return new NewException(RootException.Message + (Trace ? " Exception Occurred in Method: " + (new StackTrace()).GetFrame(1).GetMethod().Name : ""));
        }
    }
}