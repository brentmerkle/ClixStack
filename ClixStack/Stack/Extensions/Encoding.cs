using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using AppFramework;

namespace ClixStack
{
    public static class EncodingManager
    {
        #region | Encoding |

        #region > Encoded Values <

        public const string Encode_OpenParenthesis = "%~!Open~Parenthesis!~%";
        public const string Encode_CloseParenthesis = "%~!Close~Parenthesis!~%";
        public const string Encode_OpenBrace = "%~!Open~Brace!~%";
        public const string Encode_CloseBrace = "%~!Close~Brace!~%";
        public const string Encode_OpenBracket = "%~!Open~Bracket!~%";
        public const string Encode_CloseBracket = "%~!Close~Bracket!~%";
        public const string Encode_SemiColon = "%~!Semi~Colon!~%";
        public const string Encode_Comma = "%~!Comma~Comma!~%";
        public const string Encode_AmperSam = "%~!Amper~Sam!~%";
        public const string Encode_DollarSign = "%~!Dollar~Sign!~%";
        public const string Encode_Pipe = "%~!Pipe~Pipe!~%";
        public const string Encode_EqualsSign = "%~!Equal~Sign!~%";
        public const string Encode_GreaterThan = "%~!Greater~Than!~%";
        public const string Encode_LessThan = "%~!Less~Than!~%";
        public const string Encode_Astric = "%~!Astric~Astric!~%";
        public const string Encode_FowardSlash = "%~!Foward~Slash!~%";
        public const string Encode_PlusSign = "%~!Plus~Sign!~%";
        public const string Encode_MinusSign = "%~!Minus~Sign!~%";
        public const string Encode_DoubleQuote = "%~!Double~Quote!~%";

        public const string Encode_SingleQuote = "%~!Single~Quote!~%";
        public const string Encode_Upper_OR = "%~!OR!~%";
        public const string Encode_Lower_OR = "%~!or!~%";
        public const string Encode_Proper_OR = "%~!Or!~%";

        public const string Encode_RightProper_OR = "%~!oR!~%";
        public const string Encode_Upper_AND = "%~!AND!~%";
        public const string Encode_Lower_AND = "%~!and!~%";
        public const string Encode_Proper_AND = "%~!And!~%";
        public const string Encode_RightProper_AND = "%~!anD!~%";
        public const string Encode_MiddleUpper_AND = "%~!aNd!~%";
        public const string Encode_RightUpper_AND = "%~!aND!~%";

        public const string Encode_LeftUpper_AND = "%~!ANd!~%";
        public const string Encode_Upper_NOT = "%~!NOT!~%";
        public const string Encode_Lower_NOT = "%~!not!~%";
        public const string Encode_Proper_NOT = "%~!Not!~%";
        public const string Encode_RightProper_NOT = "%~!noT!~%";
        public const string Encode_MiddleUpper_NOT = "%~!nOt!~%";
        public const string Encode_RightUpper_NOT = "%~!nOT!~%";

        public const string Encode_LeftUpper_NOT = "%~!NOt!~%";
        public const string Encode_Upper_XOR = "%~!XOR!~%";
        public const string Encode_Lower_XOR = "%~!xor!~%";
        public const string Encode_Proper_XOR = "%~!Xor!~%";
        public const string Encode_RightProper_XOR = "%~!xoR!~%";
        public const string Encode_MiddleUpper_XOR = "%~!xOr!~%";
        public const string Encode_RightUpper_XOR = "%~!xOR!~%";

        public const string Encode_LeftUpper_XOR = "%~!XOr!~%";
        public const string Encode_ArrayStringDelimiter = "%~!Array~Delimiter!~%";
        public const string Encode_InnerSemiColon = "%~!Inner;Semi~Colon!~%";

        public const string Value_NoValueString = "%~!Nothing!~%";

        #endregion

        public static string xcs_Encode(this string InputString, bool BitArrayEncodeQuotes)
        {
            StringBuilder ConvertStr = new StringBuilder();
            int i = 1;
            Array StrArray = InputString.Split('\"');
            if ((StrArray != null))
            {
                foreach (object ArrSegment in StrArray)
                {
                    if (i.x_IsEven())
                    {
                        StringBuilder InnerStringSb = new StringBuilder();
                        InnerStringSb.Append(ArrSegment);
                        InnerStringSb.Replace("(", Encode_OpenParenthesis);
                        InnerStringSb.Replace(")", Encode_CloseParenthesis);
                        InnerStringSb.Replace("{", Encode_OpenBrace);
                        InnerStringSb.Replace("}", Encode_CloseBrace);
                        InnerStringSb.Replace("[", Encode_OpenBracket);
                        InnerStringSb.Replace("]", Encode_CloseBracket);
                        InnerStringSb.Replace(";", Encode_SemiColon);
                        InnerStringSb.Replace(",", Encode_Comma);
                        InnerStringSb.Replace("&", Encode_AmperSam);
                        InnerStringSb.Replace("$", Encode_DollarSign);
                        InnerStringSb.Replace("|", Encode_Pipe);
                        InnerStringSb.Replace("=", Encode_EqualsSign);
                        //Upper
                        InnerStringSb.Replace("OR", Encode_Upper_OR);
                        InnerStringSb.Replace("AND", Encode_Upper_AND);
                        InnerStringSb.Replace("NOT", Encode_Upper_NOT);
                        InnerStringSb.Replace("XOR", Encode_Upper_XOR);
                        //Proper
                        InnerStringSb.Replace("Or", Encode_Proper_OR);
                        InnerStringSb.Replace("And", Encode_Proper_AND);
                        InnerStringSb.Replace("Not", Encode_Proper_NOT);
                        InnerStringSb.Replace("Xor", Encode_Proper_XOR);
                        //Lower
                        InnerStringSb.Replace("or", Encode_Lower_OR);
                        InnerStringSb.Replace("and", Encode_Lower_AND);
                        InnerStringSb.Replace("not", Encode_Lower_NOT);
                        InnerStringSb.Replace("xor", Encode_Lower_XOR);
                        //Middle
                        InnerStringSb.Replace("aNd", Encode_MiddleUpper_AND);
                        InnerStringSb.Replace("nOt", Encode_MiddleUpper_NOT);
                        InnerStringSb.Replace("xOr", Encode_MiddleUpper_XOR);
                        //Invert Proper
                        InnerStringSb.Replace("aND", Encode_RightUpper_AND);
                        InnerStringSb.Replace("nOT", Encode_RightUpper_NOT);
                        InnerStringSb.Replace("xOR", Encode_RightUpper_XOR);
                        //Reversed Invert Proper
                        InnerStringSb.Replace("ANd", Encode_LeftUpper_AND);
                        InnerStringSb.Replace("NOt", Encode_LeftUpper_NOT);
                        InnerStringSb.Replace("XOr", Encode_LeftUpper_XOR);
                        //Reverse Proper
                        InnerStringSb.Replace("anD", Encode_MiddleUpper_AND);
                        InnerStringSb.Replace("noT", Encode_MiddleUpper_NOT);
                        InnerStringSb.Replace("xoR", Encode_MiddleUpper_XOR);
                        InnerStringSb.Append(Convert.ToChar(34) + (BitArrayEncodeQuotes ? "|" : ""));
                        InnerStringSb.Insert(0, (BitArrayEncodeQuotes ? "|" : "") + Convert.ToChar(34));
                        ConvertStr.Append(InnerStringSb.ToString());
                    }
                    else
                    {
                        StringBuilder OutterStringSb = new StringBuilder();
                        OutterStringSb.Append(ArrSegment);
                        if (BitArrayEncodeQuotes)
                        {
                            OutterStringSb.Replace("(", "|(|");
                            OutterStringSb.Replace(")", "|)|");
                            OutterStringSb.Replace("{", "|{|");
                            OutterStringSb.Replace("}", "|}|");
                            OutterStringSb.Replace("[", "|[|");
                            OutterStringSb.Replace("]", "|]|");
                            OutterStringSb.Replace(";", "|;|");
                            OutterStringSb.Replace(",", "|,|");
                            OutterStringSb.Replace("&", "|&|");
                            OutterStringSb.Replace("$", "|$|");
                            OutterStringSb.Replace("|", "|");
                            OutterStringSb.Replace("=", "|=|");
                            //Upper
                            OutterStringSb.Replace("OR", "|OR|");
                            OutterStringSb.Replace("AND", "|AND|");
                            OutterStringSb.Replace("NOT", "|NOT|");
                            OutterStringSb.Replace("XOR", "|XOR|");
                            //Propper
                            OutterStringSb.Replace("Or", "|OR|");
                            OutterStringSb.Replace("And", "|AND|");
                            OutterStringSb.Replace("Not", "|NOT|");
                            OutterStringSb.Replace("Xor", "|XOR|");
                            //Lower
                            OutterStringSb.Replace("or", "|OR|");
                            OutterStringSb.Replace("and", "|AND|");
                            OutterStringSb.Replace("not", "|NOT|");
                            OutterStringSb.Replace("xor", "|XOR|");
                            //Middle
                            OutterStringSb.Replace("aNd", "|AND|");
                            OutterStringSb.Replace("nOt", "|NOT|");
                            OutterStringSb.Replace("xOr", "|XOR|");
                            //Anti-Propper
                            OutterStringSb.Replace("aND", "|AND|");
                            OutterStringSb.Replace("nOT", "|NOT|");
                            OutterStringSb.Replace("xOR", "|XOR|");
                            //Reversed Anti-Propper
                            OutterStringSb.Replace("ANd", "|AND|");
                            OutterStringSb.Replace("NOt", "|NOT|");
                            OutterStringSb.Replace("XOr", "|XOR|");
                        }
                        ConvertStr.Append(OutterStringSb.ToString());
                    }
                    i = i + 1;
                }
            }
            else
            {
                ConvertStr.Append(InputString);
                if (BitArrayEncodeQuotes)
                {
                    ConvertStr.Replace("(", "|(|");
                    ConvertStr.Replace(")", "|)|");
                    ConvertStr.Replace("{", "|{|");
                    ConvertStr.Replace("}", "|}|");
                    ConvertStr.Replace("[", "|[|");
                    ConvertStr.Replace("]", "|]|");
                    ConvertStr.Replace(";", "|;|");
                    ConvertStr.Replace(",", "|,|");
                    ConvertStr.Replace("&", "|&|");
                    ConvertStr.Replace("$", "|$|");
                    ConvertStr.Replace("|", "|");
                    ConvertStr.Replace("=", "|=|");
                    //Upper
                    ConvertStr.Replace("OR", "|OR|");
                    ConvertStr.Replace("AND", "|AND|");
                    ConvertStr.Replace("NOT", "|NOT|");
                    ConvertStr.Replace("XOR", "|XOR|");
                    //Propper
                    ConvertStr.Replace("Or", "|OR|");
                    ConvertStr.Replace("And", "|AND|");
                    ConvertStr.Replace("Not", "|NOT|");
                    ConvertStr.Replace("Xor", "|XOR|");
                    //Lower
                    ConvertStr.Replace("or", "|OR|");
                    ConvertStr.Replace("and", "|AND|");
                    ConvertStr.Replace("not", "|NOT|");
                    ConvertStr.Replace("xor", "|XOR|");
                    //Middle
                    ConvertStr.Replace("aNd", "|AND|");
                    ConvertStr.Replace("nOt", "|NOT|");
                    ConvertStr.Replace("xOr", "|XOR|");
                    //Anti-Propper
                    ConvertStr.Replace("aND", "|AND|");
                    ConvertStr.Replace("nOT", "|NOT|");
                    ConvertStr.Replace("xOR", "|XOR|");
                    //Reversed Anti-Propper
                    ConvertStr.Replace("ANd", "|AND|");
                    ConvertStr.Replace("NOt", "|NOT|");
                    ConvertStr.Replace("XOr", "|XOR|");
                }
            }
            return ConvertStr.ToString();
        }

        public static string xcs_Decode(this string InputString)
        {
            StringBuilder ConvertStr = new StringBuilder();
            int i = 1;
            Array StrArray = InputString.Split('\"');
            foreach (string StrSegment in StrArray)
            {

                string ArrSegment = StrSegment;

                if (i.x_IsEven())
                {
                    ArrSegment = ArrSegment.Replace(Encode_OpenParenthesis, "(");
                    ArrSegment = ArrSegment.Replace(Encode_CloseParenthesis, ")");
                    ArrSegment = ArrSegment.Replace(Encode_OpenBrace, "{");
                    ArrSegment = ArrSegment.Replace(Encode_CloseBrace, "}");
                    ArrSegment = ArrSegment.Replace(Encode_OpenBracket, "[");
                    ArrSegment = ArrSegment.Replace(Encode_CloseBracket, "]");
                    ArrSegment = ArrSegment.Replace(Encode_SemiColon, ";");
                    ArrSegment = ArrSegment.Replace(Encode_Comma, ",");
                    ArrSegment = ArrSegment.Replace(Encode_DollarSign, "$");
                    ArrSegment = ArrSegment.Replace(Encode_AmperSam, "&");
                    ArrSegment = ArrSegment.Replace(Encode_EqualsSign, "=");
                    //Upper
                    ArrSegment = ArrSegment.Replace(Encode_Upper_OR, "OR");
                    ArrSegment = ArrSegment.Replace(Encode_Upper_AND, "AND");
                    ArrSegment = ArrSegment.Replace(Encode_Upper_NOT, "NOT");
                    ArrSegment = ArrSegment.Replace(Encode_Upper_XOR, "XOR");
                    //Propper
                    ArrSegment = ArrSegment.Replace(Encode_Proper_OR, "Or");
                    ArrSegment = ArrSegment.Replace(Encode_Proper_AND, "And");
                    ArrSegment = ArrSegment.Replace(Encode_Proper_NOT, "Not");
                    ArrSegment = ArrSegment.Replace(Encode_Proper_XOR, "Xor");
                    //Lower
                    ArrSegment = ArrSegment.Replace(Encode_Lower_OR, "or");
                    ArrSegment = ArrSegment.Replace(Encode_Lower_AND, "and");
                    ArrSegment = ArrSegment.Replace(Encode_Lower_NOT, "not");
                    ArrSegment = ArrSegment.Replace(Encode_Lower_XOR, "xor");
                    //Middle
                    ArrSegment = ArrSegment.Replace(Encode_MiddleUpper_AND, "aNd");
                    ArrSegment = ArrSegment.Replace(Encode_MiddleUpper_NOT, "nOt");
                    ArrSegment = ArrSegment.Replace(Encode_MiddleUpper_XOR, "xOr");
                    //Anti-Propper
                    ArrSegment = ArrSegment.Replace(Encode_RightUpper_AND, "aND");
                    ArrSegment = ArrSegment.Replace(Encode_RightUpper_NOT, "nOT");
                    ArrSegment = ArrSegment.Replace(Encode_RightUpper_XOR, "xOR");
                    //Reversed Anti-Propper
                    ArrSegment = ArrSegment.Replace(Encode_LeftUpper_AND, "ANd");
                    ArrSegment = ArrSegment.Replace(Encode_LeftUpper_NOT, "NOt");
                    ArrSegment = ArrSegment.Replace(Encode_LeftUpper_XOR, "XOr");
                    ConvertStr.Append(Convert.ToChar(34) + ArrSegment + Convert.ToChar(34));
                }
                else
                {
                    ConvertStr.Replace(Encode_InnerSemiColon, ";");
                    ConvertStr.Append(ArrSegment);
                }
                i = i + 1;
            }
            return ConvertStr.ToString();
        }

        #endregion
    }
}