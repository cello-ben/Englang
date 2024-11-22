using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Englang
{
    internal class Lexer
    {
        public enum Tokens
        {
            TOK_NULL,
            TOK_LPAREN,
            TOK_RPAREN,
            TOK_LBRACKET,
            TOK_RBRACKET,
            TOK_FUNC,
            TOK_PARAM,
            TOK_SELF,
            TOK_LITERAL
        };

        public class Token
        {
            public Tokens token;
            public string contents;
            public Token prev;
            public Token next;
            public Token()
            {
               
            }
        }
        
        public static string ExtractLiteral(char[] arr, int idx)
        {
            string res = "";
            while (idx < arr.Length)
            {
                if (arr[idx] == '"')
                {
                    break;
                }
                res += arr[idx++];
            }
            return res;
        }

        public static string ExtractFunctionName(char[] arr, int idx)
        {
            string res = "";
            while (idx < arr.Length)
            {
                if (!Char.IsLetter(arr[idx])) {
                    break;
                }
                res += arr[idx++];
            }
            return res;
        }
        public static Token TokenizeSource(string s)
        {
            char[] arr = s.ToCharArray();
            int len = arr.Length;
            Token head = new Token();
            Token dummy = head;
            int idx = 0;
            while (idx < len)
            {
                Token next = new Token();
                switch(arr[idx])
                {
                    case '\n':
                    case ' ':
                        idx++;
                        break;
                    case '{':
                        next.token = Tokens.TOK_LBRACKET;
                        next.contents = "{";
                        break;
                    case '}':
                        next.token = Tokens.TOK_RBRACKET;
                        next.contents = "}";
                        break;
                    case '(':
                        next.token = Tokens.TOK_LPAREN;
                        next.contents = "(";
                        break;
                    case ')':
                        next.token = Tokens.TOK_RPAREN;
                        next.contents = ")";
                        break;
                    case '"':
                        string literal = ExtractLiteral(arr, idx + 1);
                        idx += literal.Length;
                        next.token = Tokens.TOK_LITERAL;
                        next.contents = literal;
                        
                        break;
                    default:
                        string funcName = ExtractFunctionName(arr, idx);
                        idx += funcName.Length;
                        if (funcName == "self")
                        {
                            next.token = Tokens.TOK_SELF;
                            next.contents = "self";
                        }
                        else
                        {
                            next.token = Tokens.TOK_FUNC;
                            next.contents = funcName;
                        }
                        break;
                }
                if (next.contents != null && next.contents.Length > 0)
                {
                    next.prev = dummy;
                    dummy.next = next;
                    dummy = dummy.next;
                }
                
                idx++;
            }
            return head;
        }

        public static void _DebugPrintTokens(Token head)
        {
            while (head != null)
            {
                Console.WriteLine($"{head.token} {head.contents}");
                head = head.next;
            }
        }
    }
}
