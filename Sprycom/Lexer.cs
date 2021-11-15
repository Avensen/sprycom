namespace Sprycom
{
    class Lexer
    {
        private readonly string text;
        private int position;

        public Lexer(string _text)
        {
            this.text = _text;
        }

        private char Current
        {
            get
            {
                if (position >= text.Length)
                    return '\0';
                return text[position];
            }
        }

        private void Next()
        {
            position++;
        }


        public SyntaxToken NextToken()
        {
            if (position >= text.Length)
                return new SyntaxToken(SyntaxKind.EOFToken, position++, "\0", null);

            if (char.IsDigit(Current))
            {
                var start = position;

                while (char.IsDigit(Current))
                    Next();

                var length = position - start;
                var _text = text.Substring(start, length);
                int.TryParse(_text, out var value);

                return new SyntaxToken(SyntaxKind.NumberToken, start, _text, value);
            }

            if (char.IsWhiteSpace(Current))
            {
                var start = position;

                while (char.IsWhiteSpace(Current)) Next();

                var length = position - start;
                var _text = text.Substring(start, length);

                return new SyntaxToken(
                    SyntaxKind.WhitespaceToken, start, _text, null);
            }

            if (Current == '+')
                return new SyntaxToken(SyntaxKind.PlusToken, position++, "+", null);
            if (Current == '-')
                return new SyntaxToken(SyntaxKind.MinusToken, position++, "-", null);
            if (Current == '*')
                return new SyntaxToken(SyntaxKind.StarToken, position++, "*", null);
            if (Current == '/')
                return new SyntaxToken(SyntaxKind.SlashToken, position++, "/", null);
            if (Current == '(')
                return new SyntaxToken(SyntaxKind.OpenBracketToken, position++, "(", null);
            if (Current == ')')
                return new SyntaxToken(SyntaxKind.ClosedBracketToken, position++, ")", null);

            return new SyntaxToken(SyntaxKind.BadToken, position, text.Substring(position++, 1), null);
        }
    }
}

