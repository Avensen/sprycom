using System.Linq;
using System.Collections;
using static System.Console;
using static System.ConsoleColor;

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

                Parser parser = new Parser(line);

                var syntaxTree = parser.Parse();

                var color = ForegroundColor;
                ForegroundColor = DarkGray;
                PrettyPrint(syntaxTree.Root);
                ForegroundColor = color;

                if (syntaxTree.Diagnostics.Any())
                {
                    ForegroundColor = DarkRed;

                    foreach (var diagnostic in syntaxTree.Diagnostics)
                        WriteLine(diagnostic);

                    ForegroundColor = color;
                }
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
