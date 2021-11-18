using System.Collections.Generic;
using System.Linq;

namespace Sprycom
{
    class SyntaxToken : SyntaxNode
    {
        public override TokenKind Kind { get; }
        public int Position { get; }
        public string Text { get; }

        public object Value { get; }
        public SyntaxToken(TokenKind kind, int position, string text, object value)
        {
            Kind = kind;
            Position = position;
            Text = text;
            Value = value;
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            return Enumerable.Empty<SyntaxToken>();
        }
    }
}