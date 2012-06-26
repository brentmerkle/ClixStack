using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClixStack
{
    public enum StackObjectType
    {
        ServerOnlyCommand = 1,
        ClientOnlyCommand = 2,
        ContextCommand = 3,
        Variable = 4,
        Value = 5,
        Expression = 6,
        Math = 7,
        MathOperator = 8,
        LogicOperator = 9,
        Comparative = 10
    }
}
