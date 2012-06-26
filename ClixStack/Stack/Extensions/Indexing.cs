using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClixStack
{
    public static class IndexManager
    {

        public static Stack InitializeIndexer(this Stack Stack)
        {
            StackObjectCommandIndex StackObjectCommandIndex;

            // Jump
            StackObjectCommandIndex = new StackObjectCommandIndex();
            StackObjectCommandIndex.StackCommandType = StackObjectCommandType.Jump;
            StackObjectCommandIndex.RelatedStackCommandType = StackObjectCommandType.None;
            StackObjectCommandIndex.StackIndexType = StackIndexType.Point;
            Stack.StackObjectCommandIndexes.Add(StackObjectCommandIndex);

            // Loop
            StackObjectCommandIndex = new StackObjectCommandIndex();
            StackObjectCommandIndex.StackCommandType = StackObjectCommandType.Loop;
            StackObjectCommandIndex.RelatedStackCommandType = StackObjectCommandType.EndLoop;
            StackObjectCommandIndex.StackIndexType = StackIndexType.StartPoint;
            Stack.StackObjectCommandIndexes.Add(StackObjectCommandIndex);

            // EndLoop
            StackObjectCommandIndex = new StackObjectCommandIndex();
            StackObjectCommandIndex.StackCommandType = StackObjectCommandType.EndLoop;
            StackObjectCommandIndex.RelatedStackCommandType = StackObjectCommandType.Loop;
            StackObjectCommandIndex.StackIndexType = StackIndexType.EndPoint;
            Stack.StackObjectCommandIndexes.Add(StackObjectCommandIndex);

            // If
            StackObjectCommandIndex = new StackObjectCommandIndex();
            StackObjectCommandIndex.StackCommandType = StackObjectCommandType.If;
            StackObjectCommandIndex.RelatedStackCommandType = StackObjectCommandType.EndIf;
            StackObjectCommandIndex.StackIndexType = StackIndexType.StartPoint;
            Stack.StackObjectCommandIndexes.Add(StackObjectCommandIndex);

            // EndIf
            StackObjectCommandIndex = new StackObjectCommandIndex();
            StackObjectCommandIndex.StackCommandType = StackObjectCommandType.EndIf;
            StackObjectCommandIndex.RelatedStackCommandType = StackObjectCommandType.If;
            StackObjectCommandIndex.StackIndexType = StackIndexType.EndPoint;
            Stack.StackObjectCommandIndexes.Add(StackObjectCommandIndex);

            // Function
            StackObjectCommandIndex = new StackObjectCommandIndex();
            StackObjectCommandIndex.StackCommandType = StackObjectCommandType.Function;
            StackObjectCommandIndex.RelatedStackCommandType = StackObjectCommandType.EndFunction;
            StackObjectCommandIndex.StackIndexType = StackIndexType.StartPoint;
            Stack.StackObjectCommandIndexes.Add(StackObjectCommandIndex);

            // EndFunction
            StackObjectCommandIndex = new StackObjectCommandIndex();
            StackObjectCommandIndex.StackCommandType = StackObjectCommandType.EndFunction;
            StackObjectCommandIndex.RelatedStackCommandType = StackObjectCommandType.Function;
            StackObjectCommandIndex.StackIndexType = StackIndexType.EndPoint;
            Stack.StackObjectCommandIndexes.Add(StackObjectCommandIndex);

            return Stack;
        }

        public static Stack Index(this Stack Stack)
        {
            bool FoundInstance = false;
            string LastStackObject = "";
            string LastRelatedStackObject = "";
            string Marker = "";
            int InstanceStart = 0;
            int InstanceStop = 0;
            int Instance = 0;
            int Step = 0;
            StackObjectIndex StackObjectIndex = new StackObjectIndex();
            StackObjectCommandIndex CurrentIndex;
            try
            {
                foreach (StackObject StackObject in Stack.StackObjects)
                {
                    Stack.StackIndex++;
                    CurrentIndex = GetStackObjectIndex(Stack);
                    if ((Stack.StackObjectCommandIndexes != null))
                    {
                        LastStackObject = Stack.StackObjectIndexes.StackObjectCommandTypes[Stack.StackIndex].ToString();
                        LastRelatedStackObject = Stack.StackObjectIndexes.RelatedStackObjectCommandTypes[Stack.StackIndex].ToString();
                        switch (Stack.StackObjectCommandIndexes[Stack.StackIndex].StackIndexType)
                        {
                            case StackIndexType.Point:
                                Marker = Stack.StackObjects[Stack.StackIndex].Children[0].StackObjectValue;
                                StackObjectIndex.AddNewIndex(Marker, InstanceStart, InstanceStop, Instance, Step, CurrentIndex.StackCommandType, CurrentIndex.RelatedStackCommandType, "", "");
                                Marker = "";
                                break;
                            case StackIndexType.StartPoint:
                                Instance++;
                                InstanceStart++;
                                Step++;
                                StackObjectIndex.AddNewIndex(Marker, InstanceStart, InstanceStop, Instance, Step, CurrentIndex.StackCommandType, CurrentIndex.RelatedStackCommandType, "", "");
                                InstanceStart -= 1;
                                break;
                            case StackIndexType.EndPoint:
                                while (FoundInstance == false)
                                {
                                    if (StackObjectIndex.HasStopInstance(Instance))
                                    {
                                        Instance -= 1;
                                        if (Instance == 0)
                                        {
                                            //THROW ERROR - MISSING START POINT
                                            throw new ExceptionManager.NewException("StackIndexingException: The StackObject: '" + CurrentIndex.StackCommandType.ToString() + "' is missing the starting StackObject: '" + CurrentIndex.RelatedStackCommandType.ToString() + "' at StackIndex: '" + Stack.StackIndex.ToString() + "'.");
                                        }
                                    }
                                    else
                                    {
                                        InstanceStop = Instance;
                                        if (!StackObjectIndex.HasMatchingStartInstance(CurrentIndex.StackCommandType, InstanceStop))
                                        {
                                            //THROW ERROR - MISSING START POINT
                                            throw new ExceptionManager.NewException("StackIndexingException: The StackObject: '" + CurrentIndex.StackCommandType.ToString() + "' is missing the starting StackObject: '" + CurrentIndex.RelatedStackCommandType.ToString() + "' at StackIndex: '" + Stack.StackIndex.ToString() + "'.");
                                        }
                                        FoundInstance = true;
                                        Instance -= 1;
                                        StackObjectIndex.AddNewIndex(Marker, InstanceStart, InstanceStop, Instance, Step, CurrentIndex.StackCommandType, CurrentIndex.RelatedStackCommandType, "", "");
                                        InstanceStop = Instance;
                                    }
                                }
                                FoundInstance = false;
                                break;
                            case StackIndexType.FunctionStartPoint:
                                //Index A UserFunction
                                if (StackObjectIndex.HasStopInstance(Instance))
                                {
                                    Instance -= 1;
                                    if (Instance == 0)
                                    {
                                        //THROW ERROR - MISSING START POINT
                                        throw new ExceptionManager.NewException("StackIndexingException: The StackObject: '" + CurrentIndex.StackCommandType.ToString() + "' is missing the starting StackObject: '" + CurrentIndex.RelatedStackCommandType.ToString() + "' at StackIndex: '" + Stack.StackIndex.ToString() + "'.");
                                    }
                                }
                                else
                                {
                                    InstanceStop = Instance;
                                    if (!StackObjectIndex.HasMatchingStartInstance(CurrentIndex.StackCommandType, InstanceStop))
                                    {
                                        //THROW ERROR - MISSING START POINT
                                        throw new ExceptionManager.NewException("StackIndexingException: The StackObject: '" + CurrentIndex.StackCommandType.ToString() + "' is missing the starting StackObject: '" + CurrentIndex.RelatedStackCommandType.ToString() + "' at StackIndex: '" + Stack.StackIndex.ToString() + "'.");
                                    }
                                    FoundInstance = true;
                                    Instance -= 1;
                                    StackObjectIndex.AddNewIndex(Marker, InstanceStart, InstanceStop, Instance, Step, CurrentIndex.StackCommandType, CurrentIndex.RelatedStackCommandType, "", Stack.StackObjects[Stack.StackIndex].StackObjectValue);
                                }
                                break;
                            case StackIndexType.FunctionEndPoint:
                                //Index A UserFunctionEndPoint
                                Instance++;
                                InstanceStart++;
                                Step++;
                                StackObjectIndex.AddNewIndex(Marker, InstanceStart, InstanceStop, Instance, Step, CurrentIndex.StackCommandType, CurrentIndex.RelatedStackCommandType, Stack.StackObjects[Stack.StackIndex].StackObjectValue, "");
                                break;
                        }
                    }
                    else
                    {
                        StackObjectIndex.AddNewIndex(Marker, InstanceStart, InstanceStop, Instance, Step, StackObjectCommandType.None, StackObjectCommandType.None, "", "");
                    }
                }
                if (Instance > 0)
                {
                    //THROW ERROR - MISSING END POINT(s)
                    throw new ExceptionManager.NewException("StackIndexingException: The StackObject: '" + LastStackObject + "' is missing the ending StackObject: '" + LastRelatedStackObject + "' at StackIndex: '" + Stack.StackIndex.ToString() + "'.");
                }
                
            }
            catch (Exception)
            {
                throw;
            }
            return Stack;
        }

        private static bool HasStopInstance(this StackObjectIndex StackIndex, int InstanceStart)
        {
            try
            {
                if (StackIndex.InstanceStops.IndexOf(InstanceStart) > -1)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            { throw; }
        }

        private static bool HasMatchingStartInstance(this StackObjectIndex StackIndex, StackObjectCommandType StackObjectCommandType, int StopInstance)
        {
            try
            {
                int i = StackIndex.InstanceStarts.IndexOf(StopInstance);
                if (i > -1)
                {
                    if (StackIndex.RelatedStackObjectCommandTypes[i] == StackObjectCommandType)
                        return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static StackObjectCommandIndex GetStackObjectIndex(Stack Stack)
        {
            if (Stack.StackObjectCommandIndexes.Count > 0)
            {
                foreach (StackObjectCommandIndex Index in Stack.StackObjectCommandIndexes)
                {
                    if (Index.StackCommandType == Stack.StackObjects[Stack.StackIndex].StackObjectCommandType)
                    {
                        return Index;
                    }
                }
            }
            return null;
        }

        private static StackObjectIndex AddNewIndex(this StackObjectIndex StackObjectIndex, string Marker, int InstanceStart, int InstanceStop, int Instance, int Step, StackObjectCommandType Command, StackObjectCommandType RelatedCommand, string UserCommand, string UserEndCommand)
        {
            try
            {
                StackObjectIndex.Markers.Add(Marker);
                StackObjectIndex.UserFunctionStarts.Add(UserCommand);
                StackObjectIndex.UserFunctionStops.Add(UserEndCommand);
                StackObjectIndex.InstanceStarts.Add(InstanceStart);
                StackObjectIndex.InstanceStops.Add(InstanceStop);
                StackObjectIndex.Instances.Add(Instance);
                StackObjectIndex.Steps.Add(Step);
                StackObjectIndex.StackObjectCommandTypes.Add(Command);
                StackObjectIndex.RelatedStackObjectCommandTypes.Add(RelatedCommand);
            }
            catch (Exception)
            {
                throw;
            }
            return StackObjectIndex;
        }

        private static int GetMarkerPosition(StackObjectIndex StackObjectIndex, string Marker)
        {
            return StackObjectIndex.Markers.IndexOf(Marker);
        }

        private static int GetInstanceStartPosition(StackObjectIndex StackObjectIndex, int InstanceStart)
        {
            return StackObjectIndex.InstanceStarts.IndexOf(InstanceStart);
        }

        private static int GetInstanceStopPosition(StackObjectIndex StackObjectIndex, int InstanceStop)
        {
            return StackObjectIndex.InstanceStops.IndexOf(InstanceStop);
        }

        private static int FindMarker(StackObjectIndex StackObjectIndex, string MarkerName)
        {
            return StackObjectIndex.Markers.IndexOf(MarkerName);
        }

        private static bool HasMarker(StackObjectIndex StackObjectIndex, string Marker)
        {
            if (!(StackObjectIndex.Markers.IndexOf(Marker) == -1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool HasUserCommand(StackObjectIndex StackObjectIndex, string UserCommand)
        {
            if (!(StackObjectIndex.UserFunctionStarts.IndexOf(UserCommand) == -1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static int GetUserCommandStartPosition(StackObjectIndex StackObjectIndex, string UserCommand)
        {
            return StackObjectIndex.UserFunctionStarts.IndexOf(UserCommand);
        }

    }
}
