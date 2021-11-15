using static System.Console;
namespace Sprycom
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Write("> ");
                var line = ReadLine();

                if (string.IsNullOrWhiteSpace(line))
                    return;

                Lexer lexer = new Lexer(line);
                while (true)
                {
                    var token = lexer.NextToken();
                    if (token.Kind == TokenKind.EOFToken)
                        break;
                    Write($"{token.Kind}: {token.Text }");
                    if (token.Value != null)
                        Write($"- {token.Value}");
                    WriteLine();
                }
            }
        }
    }
}
