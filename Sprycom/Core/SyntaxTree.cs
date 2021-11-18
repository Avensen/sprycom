using System.Linq;
using System.Collections.Generic;

namespace Sprycom.Core
{
    sealed class SyntaxTree
    {
        public SyntaxTree(IEnumerable<string> diagnostics, ExpressionSyntax root, SyntaxToken EOFToken)
        {
            Diagnostics = diagnostics.ToArray();
            Root = root;
            this.EOFToken = EOFToken;
        }

        public IReadOnlyList<string> Diagnostics { get; }
        public ExpressionSyntax Root { get; }
        public SyntaxToken EOFToken { get; }

        public static SyntaxTree Parse(string sentence)
        {
            var parser = new Parser(sentence);
            return parser.Parse();
        }
    }
}