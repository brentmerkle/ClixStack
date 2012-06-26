using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClixStack
{
    public class StackObject
    {
        public StackObjectType ObjectType { get; set; }
        public string StackObjectValue { get; set; }
        public StackObjectValueType StackObjectValueType { get; set; }
        public StackObjectCommandType StackObjectCommandType { get; set; }
        public List<StackObject> Children { get; set; }
        public StackObjectMathOperatorType StackObjectMathOperatorType { get; set; }
        public StackObjectComparativeType StackObjectComparativeType { get; set; }
        public StackObjectLogicOperatorType StackObjectLogicOperatorType { get; set; }
        public string StackObjectDataName { get; set; }
        public int StackObjectDataIndex { get; set; }

    }
}
