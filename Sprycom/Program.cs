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

        static void PrettyPrint(SyntaxNode node, string indent = "", bool isLast = true)
        {
            var marker = isLast ? "└──" : "├──";
            Write(indent);
            Write(marker);
            Write(node.Kind);

            if (node is SyntaxToken t && t.Value != null)
                Write(" " + t.Value);

            WriteLine();

            indent += isLast ? "    " : "│   ";

            var lastChild = node.GetChildren().LastOrDefault();

            foreach (var child in node.GetChildren())
                PrettyPrint(child, indent, child == lastChild);
        }
    }
}