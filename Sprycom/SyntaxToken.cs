namespace Sprycom
{
    class SyntaxToken
    {
        public TokenKind Kind { get; }
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
    }
}

