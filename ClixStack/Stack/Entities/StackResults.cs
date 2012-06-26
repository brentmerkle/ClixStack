using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClixStack
{
    public static class StackResults
    {
        public static string GetSuccessResult(Stack Stack)
        { return String.Format("The stack '{0}' completed successfully at {1} on {2}.", Stack.StackName, DateTime.Now.ToShortTimeString(), DateTime.Now.ToShortDateString()); }
        public static string GetCheckResult(Stack Stack)
        { return String.Format("No problems were found in the stack '{0}'. Stack was checked at {1} on {2}.", Stack.StackName, DateTime.Now.ToShortTimeString(), DateTime.Now.ToShortDateString()); }
        public static string GetStopResult(Stack Stack)
        { return String.Format("The stack '{0}' stopped at {1} on {2}.", Stack.StackName, DateTime.Now.ToShortTimeString(), DateTime.Now.ToShortDateString()); }
    }
}
