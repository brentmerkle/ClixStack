using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppFramework;

namespace ClixStack
{
    public static class ExpressionManager
    {

        public static bool xcs_SolveExpression(this string ExpressionString)
        {

            // "Value" = "Value" Or 34 = 45 or "x" = "y" And 7 > 3 Or 22 < 66
            bool FoundResult = false;

            //Encode Strings
            ExpressionString = ExpressionString.xcs_Encode(false);

            if (ExpressionString.x_PatternCount("(") == ExpressionString.x_PatternCount(")"))
            {
                //Only Inner Expression Need to be Evaluated
                ExpressionString = ExpressionString.x_Replace("(", " ");
                ExpressionString = ExpressionString.Replace(")", " ");
                ExpressionString = ExpressionString.Replace(" OR", "|OR|");
                ExpressionString = ExpressionString.Replace("XOR", "|XOR|");
                ExpressionString = ExpressionString.Replace("AND", "|AND|");
                ExpressionString = ExpressionString.Replace("NOT", "|NOT|");
                ExpressionString = ExpressionString.Replace(">", "|>|");
                ExpressionString = ExpressionString.Replace("<", "|<|");
                ExpressionString = ExpressionString.Replace("=", "|=|");
                ExpressionString = ExpressionString.Replace("!=", "|!=|");

                object[] cExpressionArray = new object[1];
                int iExpression = 0;

                object[] cResultArray = new object[1];
                int iResult = 0;
                bool FloatResult = false;
                Array ExpressionArray = ExpressionString.Split('|');


                foreach (object cSegment in ExpressionArray)
                {
                    if (cSegment.ToString().Trim() == "=!" | cSegment.ToString().Trim() == "=" | cSegment.ToString().Trim() == ">" | cSegment.ToString().Trim() == "<")
                    {
                        Array.Resize(ref cExpressionArray, iExpression + 1);
                        cExpressionArray[iExpression] = cSegment.ToString().Trim();
                        iExpression += 1;
                    }
                    else if (cSegment.ToString().Trim() == "OR" | cSegment.ToString().Trim() == "AND" | cSegment.ToString().Trim() == "XOR")
                    {
                        //FloatResult = EvalBasicExpression(cExpressionArray)
                        if (iResult == 1)
                        {
                            //Eval Current Expression And Reset The Array
                            FloatResult = SolveComparativeExpression(cExpressionArray);
                            cExpressionArray = new object[1];
                            iExpression = 0;
                            Array.Resize(ref cResultArray, 3);
                            cResultArray[2] = FloatResult;
                            //Eval The Logical Chain
                            FloatResult = SolveLogicalExpression(cResultArray);
                            cResultArray = new object[2];
                            iResult = 1;
                            cResultArray[0] = FloatResult;
                            cResultArray[1] = cSegment;
                        }
                        else
                        {
                            Array.Resize(ref cResultArray, iResult + 1);
                            cResultArray[iResult] = SolveComparativeExpression(cExpressionArray);
                            iResult += 1;
                            Array.Resize(ref cResultArray, iResult + 1);
                            cResultArray[iResult] = cSegment.ToString().Trim();
                            //Clean Out The Expression Array.
                            cExpressionArray = new object[1];
                            iExpression = 0;
                        }
                    }
                    else if (cSegment.ToString().Trim() == "NOT")
                    {
                        //ReDim Preserve cResultArray(iResult)
                        cResultArray[iResult] = cResultArray[iResult] + cSegment.ToString().Trim();
                        //iResult += 1

                    }
                    else
                    {
                        Array.Resize(ref cExpressionArray, iExpression + 1);
                        cExpressionArray[iExpression] = cSegment.ToString().Trim();
                        iExpression += 1;
                    }
                    if (iExpression > 3)
                    {
                        //Missing Logical Operator
                    }
                }

                if (!(cResultArray.Length == 1))
                {
                    if (iResult == 1)
                    {
                        FloatResult = SolveComparativeExpression(cExpressionArray);
                        Array.Resize(ref cResultArray, 3);
                        cResultArray[2] = FloatResult;
                        FoundResult = SolveLogicalExpression(cResultArray);
                    }
                    else
                    {
                        Array.Resize(ref cResultArray, iResult + 1);
                        cResultArray[iResult] = SolveComparativeExpression(cExpressionArray);
                        FoundResult = SolveLogicalExpression(cResultArray);

                    }
                }
                else
                {
                    FoundResult = SolveComparativeExpression(cExpressionArray);
                }

            }
            else
            {
                //Logical Expressions Must Have One/Closing Brackets []
            }
            return FoundResult;
        }

        private static bool SolveComparativeExpression(object[] BasicExpressionArray)
        {
            bool EvalResult = false;

            if (BasicExpressionArray.Length != 3)
            {
                //Invalid Expression Arguments
            }
            else
            {
                if (BasicExpressionArray[1].ToString() == "!=")
                {
                    if (BasicExpressionArray[0].ToString() != BasicExpressionArray[2].ToString())
                        EvalResult = true;
                    else
                        EvalResult = false;
                }
                else if (BasicExpressionArray[1].ToString() == "=")
                {
                    if (BasicExpressionArray[0].ToString() == BasicExpressionArray[2].ToString())
                        EvalResult = true;
                    else
                        EvalResult = false;
                }
                else if (BasicExpressionArray[1].ToString() == ">")
                {
                    if (Convert.ToDecimal(BasicExpressionArray[0]) > Convert.ToDecimal(BasicExpressionArray[2]))
                        EvalResult = true;
                    else
                        EvalResult = false;
                }
                else if (BasicExpressionArray[1].ToString() == "<")
                {
                    if (Convert.ToDecimal(BasicExpressionArray[0]) < Convert.ToDecimal(BasicExpressionArray[2]))
                        EvalResult = true;
                    else
                        EvalResult = false;
                }
                else if (BasicExpressionArray[1].ToString() == "<=")
                {
                    if (Convert.ToDecimal(BasicExpressionArray[0]) <= Convert.ToDecimal(BasicExpressionArray[2]))
                        EvalResult = true;
                    else
                        EvalResult = false;
                }
                else if (BasicExpressionArray[1].ToString() == "=<")
                {
                    if (Convert.ToDecimal(BasicExpressionArray[0]) <= Convert.ToDecimal(BasicExpressionArray[2]))
                        EvalResult = true;
                    else
                        EvalResult = false;
                }
                else if (BasicExpressionArray[1] == ">=")
                {
                    if (Convert.ToDecimal(BasicExpressionArray[0]) >= Convert.ToDecimal(BasicExpressionArray[2]))
                        EvalResult = true;
                    else
                        EvalResult = false;
                }
                else if (BasicExpressionArray[1].ToString() == "=>")
                {
                    if (Convert.ToDecimal(BasicExpressionArray[0]) >= Convert.ToDecimal(BasicExpressionArray[2]))
                        EvalResult = true;
                    else
                        EvalResult = false;
                }
                else if (BasicExpressionArray[1].ToString() == "<>")
                {
                    if (Convert.ToDecimal(BasicExpressionArray[0]) != Convert.ToDecimal(BasicExpressionArray[2]))
                        EvalResult = true;
                    else
                        EvalResult = false;
                }
                else if (BasicExpressionArray[1].ToString() == "><")
                {
                    if (Convert.ToDecimal(BasicExpressionArray[0]) != Convert.ToDecimal(BasicExpressionArray[2]))
                        EvalResult = true;
                    else
                        EvalResult = false;
                }
                else
                {
                    //Invald Expression Operator
                }
            }

            return EvalResult;
        }

        private static bool SolveLogicalExpression(object[] LogicalExpressionArray)
        {
            bool EvalResult = false;
            if (LogicalExpressionArray.Length != 3)
            {
                //Invalid Expression Arguments
            }
            else
            {
                if (LogicalExpressionArray[1].ToString() == "AND")
                {
                    if ((bool)LogicalExpressionArray[0] == true & (bool)LogicalExpressionArray[2] == true)
                        EvalResult = true;
                    else
                        EvalResult = false;
                }
                else if (LogicalExpressionArray[1].ToString() == "OR")
                {
                    if ((bool)LogicalExpressionArray[0] == true | (bool)LogicalExpressionArray[2] == true)
                        EvalResult = true;
                    else
                        EvalResult = false;
                }
                else if (LogicalExpressionArray[1].ToString() == "XOR")
                {
                    if ((bool)LogicalExpressionArray[0] == true & (bool)LogicalExpressionArray[2] == true)
                    {
                        EvalResult = false;
                    }
                    if ((bool)LogicalExpressionArray[0] == true & (bool)LogicalExpressionArray[2] == false)
                    {
                        EvalResult = true;
                    }
                    if ((bool)LogicalExpressionArray[0] == false & (bool)LogicalExpressionArray[2] == true)
                    {
                        EvalResult = true;
                    }
                    if ((bool)LogicalExpressionArray[0] == false & (bool)LogicalExpressionArray[2] == false)
                    {
                        EvalResult = false;
                    }
                }
                else if (LogicalExpressionArray[1].ToString() == "ANDNOT")
                {
                    if ((bool)LogicalExpressionArray[0] == true & !((bool)LogicalExpressionArray[2] == true))
                        EvalResult = true;
                    else
                        EvalResult = false;
                }
                else if (LogicalExpressionArray[1].ToString() == "ORNOT")
                {
                    if ((bool)LogicalExpressionArray[0] == true | !((bool)LogicalExpressionArray[2] == true))
                        EvalResult = true;
                    else
                        EvalResult = false;
                }
                else if (LogicalExpressionArray[1].ToString() == "XORNOT")
                {
                    if ((bool)LogicalExpressionArray[0] == true & !((bool)LogicalExpressionArray[2] == true))
                    {
                        EvalResult = false;
                    }
                    if ((bool)LogicalExpressionArray[0] == true & !((bool)LogicalExpressionArray[2] == false))
                    {
                        EvalResult = true;
                    }
                    if ((bool)LogicalExpressionArray[0] == false & !((bool)LogicalExpressionArray[2] == true))
                    {
                        EvalResult = true;
                    }
                    if ((bool)LogicalExpressionArray[0] == false & !((bool)LogicalExpressionArray[2] == false))
                    {
                        EvalResult = false;
                    }
                }
                else
                {
                    //Invald Expression Operator
                }
            }

            return EvalResult;
        }

    }
}