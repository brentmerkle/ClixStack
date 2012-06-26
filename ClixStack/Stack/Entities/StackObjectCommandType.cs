using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClixStack
{
    public enum StackObjectCommandType
    {
        None = 0,
        Function = 1,
        EndFunction = 2,
        Loop = 3,
        EndLoop = 4,
        If = 5,
        EndIf = 6,
        Comment = 7,
        Jump = 8,
        JumpTo = 9,
        WebServiceStartCall = 10, // External WebService Start Point Command
        WebServiceStopCall = 11, // External WebService Stop Point Command
        WebServiceCall = 12, // External WebService Point Command (Sends The 
        Stop = 13,
        UpdateData = 14,
        DeleteData = 15

    }
}
