using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Reflection;
using System.Text;

namespace ClixStack
{
    public class StackManager
    {

        //public object Clone()
        //{
        //    StackManager CSM = new StackManager();

        //    CSM.LineNumber = LineNumber;
        //    CSM.ErrorStatus = ErrorStatus;
        //    CSM.ErrorMessage = ErrorMessage;
        //    CSM.CommandName = CommandName;
        //    CSM.CommandParameters = CommandParameters;
        //    CSM.CommandResult = CommandResult;
        //    CSM.Version = Version;
        //    CSM.ShowDialogMessages = ShowDialogMessages;
        //    CSM.AddMethodInfo = AddMethodInfo;
        //    CSM.Variables = Variables;
        //    CSM.IndexInstance = IndexInstance;
        //    CSM.ReturnMarkers = ReturnMarkers;
        //    CSM.Markers = Markers;
        //    CSM.InstanceStarts = InstanceStarts;
        //    CSM.InstanceStops = InstanceStops;
        //    CSM.Instances = Instances;
        //    CSM.Steps = Steps;
        //    CSM.CommandIndexes = CommandIndexes;
        //    CSM.CommandList = CommandList;
        //    CSM.RelatedCommands = RelatedCommands;

        //    return CSM;
        //}

        //private int _LineNumber;
        //private int _ErrorStatus = 0;
        //private string _ErrorMessage = "";
        //private string _CommandName = "";
        //private object[] _CommandParameters;
        //private object _CommandResult;
        //private string _Version;
        //private bool _ShowDialogMessages;
        //private bool _AddMethodInfo;
        //private string[] _ScriptBuffer;
        //private string[] _UserCommandsBuffer;

        //private bool _DebuggingEnabled;
        //public bool ShowDialogMessages
        //{
        //    get { return _ShowDialogMessages; }
        //    set { _ShowDialogMessages = value; }
        //}
        //public bool DebuggingEnabled
        //{
        //    get { return _DebuggingEnabled; }
        //    set { value = _DebuggingEnabled; }
        //}
        //public int LineNumber
        //{
        //    get { return _LineNumber; }
        //    set { _LineNumber = value; }
        //}
        //public int ErrorStatus
        //{
        //    get { return _ErrorStatus; }
        //    set { _ErrorStatus = value; }
        //}
        //public string ErrorMessage
        //{
        //    get { return _ErrorMessage; }
        //    set { _ErrorMessage = value; }
        //}
        //public string CommandName
        //{
        //    get { return _CommandName; }
        //    set { _CommandName = value; }
        //}
        //public object[] CommandParameters
        //{
        //    get { return _CommandParameters; }
        //    set { _CommandParameters = value; }
        //}
        //public object CommandResult
        //{
        //    get { return _CommandResult; }
        //    set { _CommandResult = value; }
        //}
        //public string Version
        //{
        //    get { return _Version; }
        //    set { _Version = value; }
        //}
        //public bool AddMethodInfo
        //{
        //    get { return _AddMethodInfo; }
        //    set { _AddMethodInfo = value; }
        //}
        //public string[] ScriptBuffer
        //{
        //    get { return _ScriptBuffer; }
        //    set { _ScriptBuffer = value; }
        //}

        #region < ||| Variables                             ////// >

        private List<Variable> _Variables = new List<Variable>();
        public List<Variable> Variables
        {
            get { return _Variables; }
            set { _Variables = value; }
        }
        public class Variable
        {

            private string _Name;

            private List<VariableValue> _Values = new List<VariableValue>();
            public string Name
            {
                get { return _Name; }
                set { _Name = value; }
            }
            public List<VariableValue> Values
            {
                get { return _Values; }
                set { _Values = value; }
            }

        }
        public class VariableValue
        {


            private string _Value;
            public string Value
            {
                get { return _Value; }
                set { _Value = value; }
            }

        }

        private Variable GetVariableInstance(string VariableName)
        {
            foreach (Variable Variable in Variables)
            {
                if (Variable.Name == VariableName)
                    return Variable;
            }
            return null;
        }

        private bool VariableItterationExist(Variable Variable, int Itteration)
        {
            if (Variable.Values.Count >= Itteration)
                return true;
            else
                return false;
        }

        public void PrepareVaraible(string VariableName, int Itterations)
        {
            Variable Variable = GetVariableInstance(VariableName);
            if ((Variable != null))
            {
                if (!VariableItterationExist(Variable, Itterations))
                {
                    ExpandVariableDepth(Variable, Itterations);
                }
            }
            else
            {
                CreateVariable(VariableName, Itterations);
            }
        }

        public void SetVariable(string VariableName, int Itteration, string Value)
        {
            Variable Variable = GetVariableInstance(VariableName);
            if (!VariableItterationExist(Variable, Itteration))
            {
                ExpandVariableDepth(Variable, Itteration);
            }
            Variable.Values[Itteration - 1].Value = Value;
        }

        public string GetVariable(string VariableName, int Itteration)
        {
            try
            {
                Variable Variable = GetVariableInstance(VariableName);
                if ((Variable == null))
                {
                    //ExpandVariableDepth(Variable, Itteration)
                    throw new ErrorManager.NewException("ERROR: Invalid Variable Called! The Variable '" + VariableName + "' could not be found on Line '" + LineNumber.ToString() + "'.");
                }
                if (!VariableItterationExist(Variable, Itteration))
                {
                    //ExpandVariableDepth(Variable, Itteration)
                    throw new ErrorManager.NewException("ERROR: Invalid Variable Itteration Called! The Variable '" + VariableName + "' does not have an itteration of'" + Itteration.ToString() + "' on Line '" + LineNumber.ToString() + "'.");
                }
                return Variable.Values[Itteration - 1].Value;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void ExpandVariableDepth(Variable Variable, int NewItterations)
        {
            int CurrentItterations = 0;
            VariableValue VariableValue = null;
            CurrentItterations = Variable.Values.Count;
            if (NewItterations <= CurrentItterations)
                return;
            while (!(NewItterations == CurrentItterations))
            {
                VariableValue = new VariableValue();
                VariableValue.Value = "";
                Variable.Values.Add(VariableValue);
                CurrentItterations++;
            }
        }

        private void CreateVariable(string VariableName, int Itterations)
        {
            try
            {
                Variable Variable = new Variable();
                VariableValue VariableValue = null;
                Variable.Name = VariableName;
                int CurrentItteration = 0;
                if (Itterations < 0)
                    Itterations = 1;
                while (!(CurrentItteration == Itterations))
                {
                    VariableValue = new VariableValue();
                    VariableValue.Value = "";
                    Variable.Values.Add(VariableValue);
                    CurrentItteration++;
                }
                Variables.Add(Variable);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
        #region < ||| Classes                               ////// >

        public class UserCommand
        {

            private string _UserCommandName;

            private List<UserCommandParammeter> _UserCommandParameters;
            public string UserCommandName
            {
                get { return _UserCommandName; }
                set { _UserCommandName = value; }
            }
            public List<UserCommandParammeter> UserCommandParameters
            {
                get { return _UserCommandParameters; }
                set { _UserCommandParameters = value; }
            }

        }
        public class UserCommandParammeter
        {

            private string _Name;

            private int _Index;
            public string Name
            {
                get { return _Name; }
                set { _Name = value; }
            }

            public int Index
            {
                get { return _Index; }
                set { _Index = value; }
            }

        }

        #endregion
        #region < ||| Indexing                              ////// >

        #region " ||| Data Points                        | "

        private int _IndexInstance;
        private ArrayList _ReturnMarkers = new ArrayList();
        private ArrayList _Markers = new ArrayList();
        private ArrayList _UserFunctionStarts = new ArrayList();
        private ArrayList _UserFunctionStops = new ArrayList();
        private ArrayList _InstanceStarts = new ArrayList();
        private ArrayList _InstanceStops = new ArrayList();
        private ArrayList _Instances = new ArrayList();
        private ArrayList _Steps = new ArrayList();
        private ArrayList _Command = new ArrayList();
        private ArrayList _RelatedCommand = new ArrayList();

        private List<CommandIndex> _CommandIndexes = new List<CommandIndex>();
        #endregion
        #region " ||| Properties and Classes             | "

        //public class CommandIndex
        //{

        //    private string _CommandName;
        //    private IndexType _IndexType;

        //    private string _RelatedCommandName;
        //    public string CommandName
        //    {
        //        get { return _CommandName; }
        //        set { _CommandName = value; }
        //    }

        //    public IndexType IndexType
        //    {
        //        get { return _IndexType; }
        //        set { _IndexType = value; }
        //    }

        //    public string RelatedCommandName
        //    {
        //        get { return _RelatedCommandName; }
        //        set { _RelatedCommandName = value; }
        //    }

        //}
        //public enum IndexType
        //{
        //    Point,
        //    StartPoint,
        //    EndPoint,
        //    UserFunctionStartPoint,
        //    UserFunctionEndPoint
        //}
        //private List<CommandIndex> CommandIndexes
        //{
        //    get { return _CommandIndexes; }
        //    set { _CommandIndexes = value; }
        //}
        //public int IndexInstance
        //{
        //    get { return _IndexInstance; }
        //    set { _IndexInstance = value; }
        //}
        //public ArrayList ReturnMarkers
        //{
        //    get { return _ReturnMarkers; }
        //    set { _ReturnMarkers = value; }
        //}
        //public ArrayList Markers
        //{
        //    get { return _Markers; }
        //    set { _Markers = value; }
        //}
        //public ArrayList UserFunctionStarts
        //{
        //    get { return _UserFunctionStarts; }
        //    set { _UserFunctionStarts = value; }
        //}
        //public ArrayList UserFunctionStops
        //{
        //    get { return _UserFunctionStops; }
        //    set { _UserFunctionStops = value; }
        //}
        //public ArrayList InstanceStarts
        //{
        //    get { return _InstanceStarts; }
        //    set { _InstanceStarts = value; }
        //}
        //public ArrayList InstanceStops
        //{
        //    get { return _InstanceStops; }
        //    set { _InstanceStops = value; }
        //}
        //public ArrayList Instances
        //{
        //    get { return _Instances; }
        //    set { _Instances = value; }
        //}
        //public ArrayList Steps
        //{
        //    get { return _Steps; }
        //    set { _Steps = value; }
        //}
        //public ArrayList CommandList
        //{
        //    get { return _Command; }
        //    set { _Command = value; }
        //}
        //public ArrayList RelatedCommands
        //{
        //    get { return _RelatedCommand; }
        //    set { _RelatedCommand = value; }
        //}

        #endregion

        #endregion
        #region > Execution <

        public static string ExecuteStack(Stack Stack, bool Debug, bool Trace)
        {
            try
            {
                if (!(Stack.StackObjects.Count == 0))
                {
                    string StackResult = null;
                    Stack.InitializeIndexer();
                    Stack.Index();
                    Stack.StackIndex = 0;
                    while (Stack.StackIndex < Stack.StackObjects.Count)
                    {
                        StackResult = ExecuteStackObject(Stack.StackObjects[Stack.StackIndex], !Debug).ToString();
                        if (Stack.StackObjects[Stack.StackIndex].StackObjectCommandType == StackObjectCommandType.EndFunction)
                            return StackResult;
                        Stack.StackIndex++;
                    }
                }
                if (Debug)
                    return StackResults.GetCheckResult(Stack);
                else
                    return StackResults.GetSuccessResult(Stack);
            }
            catch (Exception ex)
            {
                if ((ex.InnerException != null))
                {
                    if (!(ex.InnerException.Message == "STOP"))
                        throw ExceptionManager.HandleException(ex.InnerException, Trace);
                    else
                        if (!Debug)
                            return StackResults.GetStopResult(Stack);
                }
                else
                    throw ExceptionManager.HandleException(ex, Trace);
            }
            return null;
        }

        private static object ExecuteStackObject(StackObject StackObject, bool DoExecute)
        {
            try
            {
                
                if (StackObject.StackObjectCommandType == StackObjectCommandType.Comment || StackObject.StackObjectCommandType == StackObjectCommandType.None)
                    return null;

                if (!(StackObjectIsValid(StackObject) == true))
                {
                    throw new ErrorManager.NewException("Exception: The stack command '" + CommandName + "' is not a valid command on line: '" + LineNumber.ToString() + "'.");
                }
                if (ExecuteCommands)
                {
                    return ExecuteCommand(CommandName, CommandParameters);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }

        #endregion
        #region < ||| System Command Processing             ////// >

        public bool CommandIsValid(string CommandName)
        {
            try
            {
                // NOTE: This should handle any kind of function/method.
                CommandManager ClixCommands = new CommandManager();
                Type t = ClixCommands.GetType();
                MethodInfo m = t.GetMethod(CommandName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.IgnoreCase);
                if (m == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public object ExecuteCommand(string CommandName, object[] Parameters)
        {
            try
            {
                object[] ExeParams = new object[1];
                ExeParams[0] = this;
                CommandManager Cmds = new CommandManager();
                Type t = Cmds.GetType();
                // NOTE: This should handle any kind of function.
                MethodInfo m = t.GetMethod(CommandName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.IgnoreCase);
                // NOTE: What if method still not found?
                if (m == null)
                {
                    throw new ErrorManager.NewException("ERROR: The Command: '" + CommandName + "' is not a valid command on line: '" + LineNumber.ToString() + "'.");
                }

                //Check to see if this command is a custom function definition
                if (!(CommandName.Trim().ToLower() == "function"))
                {
                    //Parameter Count Check
                    if ((Parameters != null))
                    {
                        if (!(m.GetParameters().Length == Parameters.Length + 2))
                        {
                            throw new ErrorManager.NewException("ERROR: The Command: '" + CommandName + "' expects a different number of parammeters then provided on line: '" + LineNumber.ToString() + "'.");
                        }
                    }
                    else
                    {
                        if (!(m.GetParameters().Length == 2))
                        {
                            throw new ErrorManager.NewException("ERROR: The Command: '" + CommandName + "' expects a parammeters on line: '" + LineNumber.ToString() + "'. None were provided.");
                        }
                    }
                    //' If the Command Has Parameters Then Dynamically Call The Function and Retreive Schema and Finalize the Parameters
                    if ((Parameters != null))
                    {
                        //Create a Parameter with an Instance of Current Script Engine
                        object[] cParameters = new object[1];
                        cParameters[0] = this;
                        //Create an Array of Null Parameters
                        foreach (object Obj in Parameters)
                        {
                            Array.Resize(ref cParameters, cParameters.Length + 1);
                            cParameters[cParameters.Length - 1] = null;
                        }
                        //Add SchemaReturn Flag and Set = True
                        Array.Resize(ref cParameters, cParameters.Length + 1);
                        cParameters[cParameters.Length - 1] = true;
                        string[] SchemaArray = m.Invoke(Cmds, cParameters).ToString().Split(';');
                        //Parameters = FinalizeParameters(Parameters, SchemaArray)
                        Parameters = FinalizeParameters(Parameters, SchemaArray);
                    }

                    if ((Parameters != null))
                    {
                        foreach (object Obj in Parameters)
                        {
                            Array.Resize(ref ExeParams, ExeParams.Length + 1);
                            ExeParams[ExeParams.Length - 1] = Obj;
                        }
                    }
                    Array.Resize(ref ExeParams, ExeParams.Length + 1);

                    ///// CONDITIONAL
                    ExeParams[ExeParams.Length - 1] = false;


                    return m.Invoke(Cmds, ExeParams);
                    // Dynamically Call The Command Function.
                }
                else
                {
                    if ((Parameters != null))
                    {
                        string[] SchemaArray = null;
                        foreach (object Parameter in Parameters)
                        {
                            if ((SchemaArray != null))
                            {
                                Array.Resize(ref SchemaArray, SchemaArray.Length + 1);
                            }
                            else
                            {
                                SchemaArray = new string[1];
                            }
                            SchemaArray[SchemaArray.Length - 1] = "string";
                        }

                        if (Parameters.Length > 1)
                        {
                            //FIX !!!!!!!!!!!!!!!!!!!!!!
                            Parameters = FinalizeParameters(Parameters, SchemaArray);
                            //Re-Insert Parameter 0
                        }
                        Parameters[0] = Parameters[0].ToString().Trim();
                        int cInst = 0;
                        foreach (object Obj in Parameters)
                        {
                            Array.Resize(ref ExeParams, ExeParams.Length + 1);
                            if (cInst > 0)
                            {
                                string[] ObjInst = new string[1];
                                ObjInst[0] = Obj.ToString();
                                ExeParams[ExeParams.Length - 1] = ObjInst;
                            }
                            else
                            {
                                ExeParams[ExeParams.Length - 1] = Obj;
                            }
                            cInst++;
                        }
                    }
                    return m.Invoke(Cmds, ExeParams);
                    // Dynamically Call The Command Function.
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        #region < ||| Parameters                            ////// >
        public Object[] ParameterParser(string CommandLineString)
        {
            try
            {
                EncodingManager Encode = new EncodingManager();
                string ParameterString = ExtensionManager.Window(CommandLineString, ExtensionManager.Position(CommandLineString, "(", 1) + 1, ExtensionManager.Position(CommandLineString, ")", ExtensionManager.PatternCount(CommandLineString, ")"))).Trim();
                ParameterString.Replace("\\" + Convert.ToChar(34), Encode.DoubleQuote);
                //Encode Double Quotes
                if (ParameterString.Contains(Convert.ToChar(34).ToString()))
                {
                    if (ExtensionManager.PatternCount(ParameterString, (Convert.ToChar(34)).ToString()) / 2 == (ExtensionManager.PatternCount(ParameterString, (Convert.ToChar(34)).ToString()) / 2))
                    {
                        ParameterString = Encode.StringEncoder(ParameterString, false);
                    }
                    else
                    {
                        throw new ErrorManager.NewException("ERROR: The Command: '" + _CommandName + "' has an invalid number of double-quotations within a parammeter on line: '" + LineNumber.ToString() + "'. Be sure to use the double-quote escape '/\"' when placing double quotes within a string.");
                    }
                }
                //Encode Semi-Colon Within Inner Bracketed Commands
                if (ExtensionManager.PatternCount(ParameterString, ";") > 0)
                {
                    //Detect Bracketed Inner Commands
                    if (ParameterString.Contains("(") | ParameterString.Contains(")"))
                    {
                        //Validate Proper Inner Bracket Syntax
                        if (ExtensionManager.PatternCount(ParameterString, "(") == ExtensionManager.PatternCount(ParameterString, ")"))
                        {
                            ParameterString = ";" + ParameterString + ";";
                            //Add a Leading and Trailing Semi-Colon For Encoding Process
                            int SemiColonCount = ExtensionManager.PatternCount(ParameterString, ";");
                            bool Flag = false;
                            int ParseInstance = 1;
                            int InstanceStep = 1;
                            Flag = false;
                            //Scenario ;....;... Until Found Bracket Balance
                            while (Flag == false)
                            {
                                if (!(ExtensionManager.PatternCount(ExtensionManager.Window(ParameterString, ExtensionManager.Position(ParameterString, ";", ParseInstance), ExtensionManager.Position(ParameterString, ";", ParseInstance + InstanceStep)), "(") == ExtensionManager.PatternCount(ExtensionManager.Window(ParameterString, ExtensionManager.Position(ParameterString, ";", ParseInstance), ExtensionManager.Position(ParameterString, ";", ParseInstance + InstanceStep)), ")")))
                                {
                                    InstanceStep = InstanceStep + 1;
                                    //If Open to Close Bracket Count Mis-Matches Increase the Step Instance of the Semi-Colons
                                }
                                else
                                {
                                    int i = 0;
                                    //If Open to Close Bracket Count Matches then Encode and Increase the Instance of the Semi-Colon
                                    i = ParameterString.Length;
                                    ParameterString = ExtensionManager.WindowReplace(
                                        ParameterString,
                                        ExtensionManager.Position(
                                        ParameterString,
                                        ";",
                                        ParseInstance) + 1,
                                        ExtensionManager.Position(
                                        ParameterString,
                                        ";",
                                        ParseInstance + InstanceStep),
                                        ";",
                                        Encode.InnerSemiColon);
                                    //i = ParameterString.Length
                                    //ParameterString = ";" & ParameterString & ";"
                                    if (InstanceStep > 1)
                                    {
                                        ParseInstance = ParseInstance + InstanceStep;
                                        InstanceStep = 1;
                                    }
                                    else
                                    {
                                        ParseInstance = ParseInstance + 1;
                                    }
                                }
                                if (ParseInstance == SemiColonCount)
                                    Flag = true;
                                //If ParseInstance = ParameterString.PatternCount(";") Then Flag = True
                            }
                            ParameterString = ExtensionManager.Left(ParameterString, ParameterString.Length - 1);
                            //Remove Leading and Trailing Semi-Colon
                            ParameterString = ExtensionManager.Right(ParameterString, ParameterString.Length - 1);
                            ParameterString = ParameterString.Replace(Encode.InnerSemiColon, Encode.SemiColon);
                        }
                        else
                        {
                            //Error Invalid Encode of "(" or ")" Character Found!
                            throw new ErrorManager.NewException("ERROR: The Command: '" + CommandName + "' has an invalid number of brackets '()' within a parammeter on line: '" + LineNumber.ToString() + "'.");
                        }
                    }
                }
                string[] ParamArr = ParameterString.Split(';');
                for (int i = 0; i <= ParamArr.Length - 1; i++)
                {
                    ParamArr[i] = ParamArr[i].Replace(Encode.SemiColon, ";");
                    ParamArr[i] = Encode.StringDecoder(ParamArr[i]);
                }
                return ParamArr;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private object[] FinalizeParameters(object[] ParammeterArray, string[] SchemaArray)
        {
            try
            {
                int cParam = ParammeterArray.Length - 1;
                object[] FinalizedParms = null;
                int i = 0;
                object cParameter = null;
                string cSchema = null;
                for (i = 0; i <= cParam; i++)
                {
                    cParameter = ParammeterArray[i];
                    cSchema = SchemaArray[i];
                    switch (cSchema)
                    {
                        case "string":
                            cParameter = ParameterEvaluation(cParameter.ToString()).Trim();
                            if (!(ExtensionManager.Left(cParameter.ToString().Trim(), 1) == "\"") | !(ExtensionManager.Right(cParameter.ToString().Trim(), 1) == "\""))
                            {
                                throw new ErrorManager.NewException("ERROR: Non Encapsulated string found on line: '" + LineNumber.ToString() + "'. The value found was: '" + cParameter + "'.");
                                //if (!(ExtensionManager.Left(cParameter.ToString().Trim(), 1) == "\""))
                                //    cParameter = "\"" + cParameter;
                                //if (!(ExtensionManager.Right(cParameter.ToString().Trim(), 1) == "\""))
                                //    cParameter = cParameter + "\"";
                            }
                            cParameter = ParameterEvaluation(cParameter.ToString());
                            cParameter = cParameter.ToString().Trim();
                            if (cParameter.ToString().Length > 2)
                                cParameter = ExtensionManager.Window(cParameter.ToString(), 2, cParameter.ToString().Length);
                            break;
                        case "bool":
                            try
                            {
                                cParameter = Convert.ToBoolean(ParameterEvaluation(cParameter.ToString()).Trim());
                            }
                            catch (Exception ex)
                            {
                                if (!ex.GetType().ToString().Contains("TargetInvocationException"))
                                {
                                    throw new ErrorManager.NewException("ERROR: Non boolean parameter value found on line: '" + LineNumber.ToString() + "'. The value found was: '" + cParameter + "'.");
                                }
                                else
                                {
                                    throw;
                                }
                            }
                            break;
                        case "command":
                            cParameter = cParameter.ToString().Trim();
                            break;
                        case "number":
                            try
                            {
                                cParameter = Convert.ToDouble(ParameterEvaluation(cParameter.ToString()));
                            }
                            catch (Exception ex)
                            {
                                if (!ex.GetType().ToString().Contains("TargetInvocationException"))
                                {
                                    throw new ErrorManager.NewException("ERROR: Non numerical parameter found on line: '" + LineNumber.ToString() + "'. The parameter value found was: '" + cParameter + "'.");
                                }
                                else
                                {
                                    throw;
                                }
                            }
                            break;
                        case "variablename":
                            cParameter = cParameter.ToString().Trim();
                            if (ExtensionManager.Left(cParameter.ToString().Trim(), 1) == "\"" & ExtensionManager.Right(cParameter.ToString().Trim(), 1) == "\"")
                            {
                                if (cParameter.ToString().Length > 2)
                                    cParameter = ExtensionManager.Window(cParameter.ToString(), 2, cParameter.ToString().Length);
                            }
                            if (!(ExtensionManager.Left(cParameter.ToString().Trim(), 1) == "$"))
                            {
                                throw new ErrorManager.NewException("ERROR: The parameter Variable name: '" + cParameter + "' does not begin with '$' on line: '" + LineNumber.ToString() + "'.");
                            }
                            if (cParameter.ToString().Contains(" "))
                            {
                                throw new ErrorManager.NewException("ERROR: Space character(s) found in the parameter Variable name: '" + cParameter + "' on line: '" + LineNumber.ToString() + "'. Variable names can't have space characters!");
                            }
                            break;
                        case "linename":
                            cParameter = cParameter.ToString().Trim();
                            if (ExtensionManager.Left(cParameter.ToString().Trim(), 1) == "\"" & ExtensionManager.Right(cParameter.ToString().Trim(), 1) == "\"")
                            {
                                if (cParameter.ToString().Length > 2)
                                    cParameter = ExtensionManager.Window(cParameter.ToString(), 2, cParameter.ToString().Length);
                            }
                            if (string.IsNullOrEmpty(cParameter.ToString()))
                            {
                                throw new ErrorManager.NewException("ERROR: parameter Line name was blank on line: '" + LineNumber.ToString() + "'.");
                            }
                            if (cParameter.ToString().Contains(" "))
                            {
                                throw new ErrorManager.NewException("ERROR: Space character(s) found in the parameter line name: '" + cParameter + "' on line: '" + LineNumber.ToString() + "'. Line names can't have space characters!");
                            }
                            break;
                        case "exspression":
                            cParameter = ParameterEvaluation(cParameter.ToString().Trim()).Trim();
                            //if (ExtensionManager.Left(cParameter.ToString().Trim(), 1) == "\"" & ExtensionManager.Right(cParameter.ToString().Trim(), 1) == "\"")
                            //{
                            //    if (cParameter.ToString().Length > 2)
                            //        cParameter = ExtensionManager.Window(cParameter.ToString(), 2, cParameter.ToString().Length);
                            //}
                            break;
                    }
                    if ((FinalizedParms == null))
                    {
                        FinalizedParms = new object[1];
                    }
                    else
                    {
                        Array.Resize(ref FinalizedParms, FinalizedParms.Length + 1);
                    }
                    FinalizedParms[FinalizedParms.Length - 1] = cParameter;
                }
                return FinalizedParms;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public enum ConcatenationPhase : byte
        {
            Search = 1,
            Concatenate,
            Buffer
        }


        public string ParameterEvaluation(string ParameterString)
        {
            // Output!
            StringBuilder SbCommandProcessingResult = new StringBuilder();
            StringBuilder SbConcatenationResult = new StringBuilder();
            StringBuilder dynamic = new StringBuilder();
            StringBuilder SbCommand = new StringBuilder();
            EncodingManager Encode = new EncodingManager();
            //string TestResultStr = null;
            //string TestCommandStr = null;

            try
            {
                int Cycle = 0;
                bool Processing = false;
                ParameterString = Encode.StringEncoder(ParameterString, true);
                string[] ParameterArray = ParameterString.Split('|');
                ParameterArray = ExtensionManager.ArrayTrim(ParameterArray, true);
                if (!string.IsNullOrEmpty(ParameterString.Trim()))
                {
                    for (int i = 1; i <= ParameterArray.Length; i++)
                    {
                        if (CommandIsValid(ParameterArray[i - 1]) == true)
                        {
                            SbCommand.Append(ParameterArray[i - 1]);
                            Cycle++;
                            if (ParameterArray.Length > 1)
                            {
                                if (ParameterArray[i] == "(")
                                {
                                    Processing = true;
                                    //'Keep Adding Values from the Array until the command is balanced in terms of braces.
                                    while (Processing)
                                    {
                                        if (!(ExtensionManager.Right(SbCommand.ToString(), 1) == "$"))
                                            SbCommand.Append(" ");
                                        SbCommand.Append(ParameterArray[(i - 1) + Cycle].ToString());
                                        if (!(ParameterArray[(i - 1) + Cycle].ToString() == "$"))
                                            SbCommand.Append(" ");
                                        Cycle++;
                                        //TestCommandStr = SbCommand.ToString();
                                        if (ExtensionManager.PatternCount(SbCommand.ToString(), "(") == ExtensionManager.PatternCount(SbCommand.ToString(), ")"))
                                        {
                                            ClixScriptManager CSM = (ClixScriptManager)this.Clone();
                                            SbCommandProcessingResult.Append(" ");
                                            SbCommandProcessingResult.Append(CSM.ExecuteCommandLine(SbCommand.ToString(), true).ToString());
                                            SbCommandProcessingResult.Append(" ");
                                            //TestResultStr = SbResult.ToString();
                                            i = i + Cycle;// !!!!!!! Validate used to be  i = Cycle
                                            Cycle = 0;
                                            Processing = false;
                                        }
                                    }
                                }
                                else
                                {
                                    //Create New Instance of ClixScript Engine And Process
                                    ClixScriptManager CSM = (ClixScriptManager)this.Clone();
                                    SbCommandProcessingResult.Append(" ");
                                    SbCommandProcessingResult.Append(CSM.ExecuteCommandLine(ParameterArray[i], true).ToString());
                                    SbCommandProcessingResult.Append(" ");
                                    //TestResultStr = SbResult.ToString();
                                    Processing = false;
                                }
                            }
                            else
                            {
                                //Create New Instance of ClixScript Engine And Process
                                ClixScriptManager CSM = (ClixScriptManager)this.Clone();
                                SbCommandProcessingResult.Append(" ");
                                SbCommandProcessingResult.Append(CSM.ExecuteCommandLine(ParameterArray[i - 1], true).ToString());
                                SbCommandProcessingResult.Append(" ");
                                //TestResultStr = SbResult.ToString();
                                Processing = false;
                            }
                        }
                        else
                        {
                            if (!(ExtensionManager.Right(SbCommandProcessingResult.ToString(), 1) == "$"))
                            {
                                SbCommandProcessingResult.Append(" ");
                            }
                            SbCommandProcessingResult.Append(ParameterArray[(i - 1)].ToString());
                            if (!(ParameterArray[(i - 1) + Cycle].ToString() == "$"))
                            {
                                SbCommandProcessingResult.Append(" ");
                            }
                            //TestResultStr = SbResult.ToString();
                            if (ParameterArray.Length == 1)
                            {
                                break;
                            }
                        }
                    }
                }

                // Begin String Concatenation Processing - '&'
                ParameterString = Encode.StringEncoder(SbCommandProcessingResult.ToString(), true);
                ParameterArray = ParameterString.Split('|');
                bool buffers_full = false;
                bool is_first_concatenated_value = true;
                bool is_active_concatenation = false;
                StringBuilder concatenation_buffer = new StringBuilder(); ;
                StringBuilder output_buffer = new StringBuilder(); ;

                //int process_phase = 1;
                ConcatenationPhase current_phase = new ConcatenationPhase();
                current_phase = ConcatenationPhase.Search;

                for (int i = 0; i <= ParameterArray.Length - 1; i++)
                {
                    if (i == ParameterArray.Length - 1)
                    {
                        if (ParameterArray[i] != "&" && ParameterArray[i].Trim() != "")
                        {
                            if (current_phase == ConcatenationPhase.Search)
                            {
                                ParameterArray[i] = ParameterArray[i].Trim();

                                // Pass the Current Value to Output stream
                                ParameterArray[i] = " " + ParameterArray[i];
                                SbConcatenationResult.Append(ParameterArray[i]);
                            }
                            else if (current_phase == ConcatenationPhase.Concatenate)
                            {
                                ParameterArray[i] = ParameterArray[i].Trim();
                                ParameterArray[i] = ParameterArray[i] + "\"";
                                SbConcatenationResult.Append(ParameterArray[i]);
                            }
                        }
                        if (current_phase == ConcatenationPhase.Buffer)
                        {
                            if (is_active_concatenation)
                            {
                                // Dump the Concatenation Buffer to the Output Stream
                                //concatenation_buffer = concatenation_buffer.Trim();
                                //concatenation_buffer = concatenation_buffer + "\"";
                                concatenation_buffer.Append("\"");
                                SbConcatenationResult.Append(concatenation_buffer.ToString());
                            }
                            else
                            {
                                // Dump the Output Buffer to the Output Stream
                                //output_buffer = output_buffer.Trim();
                                //output_buffer = " " + output_buffer;
                                output_buffer.Insert(0, " ");
                                SbConcatenationResult.Append(output_buffer.ToString());
                            }
                            if (ParameterArray[i] != "&" && ParameterArray[i].Trim() != "")
                            {
                                ParameterArray[i] = ParameterArray[i].Trim();

                                // Pass the Current Value to Output stream
                                ParameterArray[i] = " " + ParameterArray[i];
                                SbConcatenationResult.Append(ParameterArray[i]);
                            }
                        }

                    }
                    else
                    {
                        if (ParameterArray[i] == "&")
                        {
                            // Concatenation Phase

                            // Push the Concatenation Buffer into the output stream
                            SbConcatenationResult.Append(concatenation_buffer);

                            // Clear Buffers
                            //output_buffer = "";
                            //concatenation_buffer = "";
                            output_buffer.Clear();
                            concatenation_buffer.Clear();
                            buffers_full = false;

                            // Set Current Phase
                            current_phase = ConcatenationPhase.Concatenate;

                            // Flag Active Concatenation Cycle
                            is_active_concatenation = true;
                        }
                        else
                        {
                            if (ParameterArray[i].Trim() == "")
                            {
                                // Store Value into Output Buffer Even if value is Whitespace
                                //output_buffer += ParameterArray[i];
                                output_buffer.Append(ParameterArray[i]);
                            }
                            else
                            {
                                if (buffers_full)
                                {
                                    // Search Phase

                                    // This Concatination Cycle has ended, dump the buffers and start a new concatenation cycle
                                    if (is_first_concatenated_value)
                                    {
                                        // No Active Concatination is occurring
                                        SbConcatenationResult.Append(output_buffer);
                                    }
                                    else
                                    {
                                        // Complete current Active Concatenation
                                        //concatenation_buffer += "\"";
                                        concatenation_buffer.Append("\"");
                                        SbConcatenationResult.Append(concatenation_buffer.ToString());
                                        is_first_concatenated_value = true;
                                    }
                                    // Clear Buffers
                                    //output_buffer = "";
                                    //concatenation_buffer = "";
                                    output_buffer.Clear();
                                    concatenation_buffer.Clear();
                                    buffers_full = false;

                                    // Set Current Phase
                                    current_phase = ConcatenationPhase.Search;

                                    // Un-Flag Active Concatenation Cycle
                                    is_active_concatenation = false;
                                }
                                else
                                {
                                    // Buffer Phase

                                    // Store Value into Concatenatenation Buffer
                                    if (is_first_concatenated_value)
                                    {
                                        ParameterArray[i] = ParameterArray[i].Trim();
                                        if (ParameterArray[i].Length >= 2 && ParameterArray[i].Left(1) == "\"" && ParameterArray[i].Right(1) == "\"")
                                        {
                                            // ""string"" -> ""string" also """" -> """
                                            //concatenation_buffer = ParameterArray[i].Left(ParameterArray[i].Length - 1);
                                            concatenation_buffer.Clear();
                                            concatenation_buffer.Append(ParameterArray[i].Left(ParameterArray[i].Length - 1));
                                        }
                                        else
                                        {
                                            // "xy" -> ""xy" or "x" -> ""x"
                                            //concatenation_buffer = "\"" + ParameterArray[i];
                                            concatenation_buffer.Clear();
                                            concatenation_buffer.Append("\"" + ParameterArray[i]);
                                        }
                                        // Set first concatenation processing flag to false (controls how the items in the buffers are encapsulated)
                                        is_first_concatenated_value = false;
                                    }
                                    else
                                    {
                                        ParameterArray[i] = ParameterArray[i].Trim();
                                        if (ParameterArray[i].Length >= 2 && ParameterArray[i].Left(1) == "\"" && ParameterArray[i].Right(1) == "\"")
                                        {
                                            // ""string"" -> "string" also """" -> ""
                                            //concatenation_buffer = ParameterArray[i].Window(1, ParameterArray[i].Length - 1);
                                            concatenation_buffer.Append(ParameterArray[i].Window(1, ParameterArray[i].Length - 1));
                                        }
                                        else
                                        {
                                            // "xy" -> "xy" or "x" -> "x"
                                            //concatenation_buffer = ParameterArray[i];
                                            concatenation_buffer.Clear();
                                            concatenation_buffer.Append(ParameterArray[i]);
                                        }
                                    }
                                    // Store Value into Output Buffer
                                    //output_buffer += ParameterArray[i];
                                    output_buffer.Append(ParameterArray[i]);
                                    buffers_full = true;

                                    // Set Current Phase
                                    current_phase = ConcatenationPhase.Buffer;
                                }
                            }
                        }
                    }
                    string str = SbConcatenationResult.ToString();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            String TestResultStr = SbConcatenationResult.ToString();
            return Encode.StringDecoder(SbConcatenationResult.ToString());
        }
        #endregion

    }
}