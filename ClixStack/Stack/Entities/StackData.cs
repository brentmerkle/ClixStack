using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClixStack
{
    public class StackData
    {
        public StackData()
        {
            StackDataDictionary = new Dictionary<string, List<object>>();
        }
        public Dictionary<string, List<object>> StackDataDictionary { get; set; }
    }
}
