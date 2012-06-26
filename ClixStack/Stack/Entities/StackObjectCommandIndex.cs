using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClixStack
{
    public class StackObjectCommandIndex
    {
        public StackObjectCommandType StackCommandType { get; set; }
        public StackObjectCommandType RelatedStackCommandType { get; set; }
        public StackIndexType StackIndexType { get; set; }
    }
}
