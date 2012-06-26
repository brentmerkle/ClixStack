using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClixStack
{
    public class StackObjectIndex
    {
        public StackObjectIndex()
        {
            InstanceStarts = new List<int>();
            InstanceStops = new List<int>();
            Instances = new List<int>();
            Steps = new List<int>();
            Markers = new List<string>();
            ReturnMarkers = new List<string>();
            UserFunctionStarts = new List<string>();
            UserFunctionStops = new List<string>();
            StackObjectCommandTypes = new List<StackObjectCommandType>();
            RelatedStackObjectCommandTypes = new List<StackObjectCommandType>();
        }
        public List<int> InstanceStarts { get; set; }
        public List<int> InstanceStops { get; set; }
        public List<int> Instances { get; set; }
        public List<int> Steps { get; set; }
        public List<string> Markers { get; set; }
        public List<string> ReturnMarkers { get; set; }
        public List<string> UserFunctionStarts { get; set; }
        public List<string> UserFunctionStops { get; set; }
        public List<StackObjectCommandType> StackObjectCommandTypes { get; set; }
        public List<StackObjectCommandType> RelatedStackObjectCommandTypes { get; set; }
    }
}
