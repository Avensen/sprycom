using System.Linq;
using System.Collections;
using static System.Console;
using static System.ConsoleColor;
using Sprycom.Core;

namespace Sprycom
{
    class Program
    {
        static void Main(string[] args)
        {
            bool showTree = false;

            while (true)
            {
                Write("> ");
                var line = ReadLine();

                if (string.IsNullOrWhiteSpace(line))
                    return;

                if (line == "--showtree")
                {
                    showTree = !showTree;
                    WriteLine(showTree ? "Showing parse tree" : "Not showing parse tree");
                    continue;
                }
                if (line == "--clear")
                {
                    Clear();
                    continue;
                }

                var syntaxTree = SyntaxTree.Parse(line);

                if (showTree)
                {
                    var color = ForegroundColor;
                    ForegroundColor = DarkGray;
                    PrettyPrint(syntaxTree.Root);
                    ForegroundColor = color;
                }

                if (!syntaxTree.Diagnostics.Any())
                {
                    var e = new Evaluator(syntaxTree.Root);
                    var result = e.Evaluate();
                    WriteLine(result);
                }
                else
                {
                    var color = ForegroundColor;
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
