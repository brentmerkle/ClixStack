using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClixStack
{
    public static class CommandManager
    {
        public static Stack Execute(this Stack s)
        {
            return new Stack();
        }

        private static Stack ExecuteStack(this Stack s)
        {
            if (s.StackObjects[s.StackIndex].StackObjectCommandType == StackObjectCommandType.Comment ||
                s.StackObjects[s.StackIndex].StackObjectCommandType == StackObjectCommandType.None)
                return s;
            else if (s.StackObjects[s.StackIndex].StackObjectCommandType == StackObjectCommandType.If)
                return If(s);
            else if (s.StackObjects[s.StackIndex].StackObjectCommandType == StackObjectCommandType.EndIf)
                return EndIf(s);
            else if (s.StackObjects[s.StackIndex].StackObjectCommandType == StackObjectCommandType.Jump)
                return Jump(s);
            else if (s.StackObjects[s.StackIndex].StackObjectCommandType == StackObjectCommandType.JumpTo)
                return JumpTo(s);
            else if (s.StackObjects[s.StackIndex].StackObjectCommandType == StackObjectCommandType.Loop)
                return Loop(s);
            else if (s.StackObjects[s.StackIndex].StackObjectCommandType == StackObjectCommandType.EndLoop)
                return EndLoop(s);
            else if (s.StackObjects[s.StackIndex].StackObjectCommandType == StackObjectCommandType.Function)
                return Function(s);
            else if (s.StackObjects[s.StackIndex].StackObjectCommandType == StackObjectCommandType.EndFunction)
                return EndFunction(s);
            else if (s.StackObjects[s.StackIndex].StackObjectCommandType == StackObjectCommandType.Stop)
                return Stop(s);
            else if (s.StackObjects[s.StackIndex].StackObjectCommandType == StackObjectCommandType.WebServiceCall)
                return WebServiceCall(s);
            else if (s.StackObjects[s.StackIndex].StackObjectCommandType == StackObjectCommandType.WebServiceStartCall)
                return WebServiceStartCall(s);
            else if (s.StackObjects[s.StackIndex].StackObjectCommandType == StackObjectCommandType.WebServiceStopCall)
                return WebServiceStopCall(s);

            throw new NotImplementedException();
        }

        private static Stack If(Stack s)
        {
            return new Stack();
        }

        private static Stack EndIf(Stack s)
        {
            return new Stack();
        }

        private static Stack Jump(Stack s)
        {
            return new Stack();
        }

        private static Stack JumpTo(Stack s)
        {
            return new Stack();
        }

        private static Stack Loop(Stack s)
        {
            return new Stack();
        }

        private static Stack EndLoop(Stack s)
        {
            return new Stack();
        }

        private static Stack Function(Stack s)
        {
            return new Stack();
        }

        private static Stack EndFunction(Stack s)
        {
            return new Stack();
        }

        private static Stack Stop(Stack s)
        {
            return new Stack();
        }

        private static Stack WebServiceCall(Stack s)
        {
            return new Stack();
        }

        private static Stack WebServiceStartCall(Stack s)
        {
            return new Stack();
        }

        private static Stack WebServiceStopCall(Stack s)
        {
            return new Stack();
        }
    }
}
