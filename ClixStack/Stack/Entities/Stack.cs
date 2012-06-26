using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClixStack
{
    public class Stack
    {
        public Stack()
        {
            ErrorStatus = 0;
            ErrorMessage = "";
            StackObjects = new List<StackObject>();
            StackObjectIndexes = new StackObjectIndex();
            StackObjectCommandIndexes = new List<StackObjectCommandIndex>();
            StackBuffers = new List<string>();
            UserStackBuffers = new List<string>();
        }
        public List<StackObject> StackObjects { get; set; }
        public StackObjectIndex StackObjectIndexes { get; set; }
        public string StackName { get; set; }
        public List<StackObjectCommandIndex> StackObjectCommandIndexes { get; set; }
        public int ErrorStatus { get; set; }
        public string ErrorMessage { get; set; }
        public List<string> StackBuffers { get; set; }
        public List<string> UserStackBuffers { get; set; }
        public int StackIndex { get; set; }
        public string StackResult { get; set; }
    }
}
