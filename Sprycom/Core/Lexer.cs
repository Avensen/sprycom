using System.Collections;
using System.Collections.Generic;

namespace Sprycom.Core
{
    class Lexer
    {
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
        private readonly string text;
        private int position;

        private List<string> diagnostics = new List<string>();

        public IEnumerable<string> Diagnostics => diagnostics;

        private void Next()
        {
            position++;
        }


        public SyntaxToken NextToken()
        {
            if (position >= text.Length)
                return new SyntaxToken(TokenKind.EOFToken, position++, "\0", null);

            if (char.IsDigit(Current))
            {
                var start = position;

                while (char.IsDigit(Current))
                    Next();

                var length = position - start;
                var _text = text.Substring(start, length);

                if (!int.TryParse(_text, out var value))
                {
                    diagnostics.Add($"ERROR: The number {text} cannot be represented by an Int32");
                }

                return new SyntaxToken(TokenKind.NumberToken, start, _text, value);
            }

            if (char.IsWhiteSpace(Current))
            {
                var start = position;

                while (char.IsWhiteSpace(Current)) Next();

                var length = position - start;
                var _text = text.Substring(start, length);

                return new SyntaxToken(
                    TokenKind.WhitespaceToken, start, _text, null);
            }

            if (Current == '+')
                return new SyntaxToken(TokenKind.PlusToken, position++, "+", null);
            if (Current == '-')
                return new SyntaxToken(TokenKind.MinusToken, position++, "-", null);
            if (Current == '*')
                return new SyntaxToken(TokenKind.StarToken, position++, "*", null);
            if (Current == '/')
                return new SyntaxToken(TokenKind.SlashToken, position++, "/", null);
            if (Current == '(')
                return new SyntaxToken(TokenKind.OpenBracketToken, position++, "(", null);
            if (Current == ')')
                return new SyntaxToken(TokenKind.ClosedBracketToken, position++, ")", null);

            diagnostics.Add($"Error: Bad character input: '{Current}'");
            return new SyntaxToken(TokenKind.BadToken, position, text.Substring(position++, 1), null);
        }
    }
}

