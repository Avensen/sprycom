using System.Linq;
using System.Collections.Generic;
namespace Sprycom
{
    sealed class SyntaxTree {
        public SyntaxTree(IEnumerable<string> diagnostics, ExpressionSyntax root, SyntaxToken EOFToken) {
            Diagnostics = diagnostics.ToArray();
            Root = root;
            this.EOFToken = EOFToken;
        }

        public IReadOnlyList<string> Diagnostics { get; }
        public ExpressionSyntax Root { get; }
        public SyntaxToken EOFToken { get; }
    }
}