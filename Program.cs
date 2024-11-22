using Englang;
using static Englang.Lexer;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: englang <path_to_source.el> <path_to_output_file.txt>");
            return;
        }
        string source = "present(self, \"be\")";
        Console.WriteLine(source);
        Token t = Lexer.TokenizeSource(source);
        Lexer._DebugPrintTokens(t);
    }
}