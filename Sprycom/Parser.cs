using System.Collections.Generic;

namespace Sprycom
{
    class Parser
    {
        public Parser(string text)
        {
            Lexer lexer = new Lexer(text);
            var _tokens = new List<SyntaxToken>();
            SyntaxToken token;

            do
            {
                token = lexer.NextToken();
                if (!(token.Kind == TokenKind.WhitespaceToken || token.Kind == TokenKind.BadToken))
                    _tokens.Add(token);

            } while (token.Kind != TokenKind.EOFToken);

            tokens = _tokens.ToArray();
            diagnostics.AddRange(lexer.Diagnostics);
        }

        private readonly SyntaxToken[] tokens;
        private int position;
        private List<string> diagnostics = new List<string>();


        public IEnumerable<string> Diagnostics => diagnostics;

        private SyntaxToken Peek(int offset)
        {
            int index = position + offset;
            if (index >= tokens.Length)
                return tokens[tokens.Length - 1];
            return tokens[index];
        }

        private SyntaxToken Current => Peek(0);

        private SyntaxToken NextToken()
        {
            var current = Current;
            position++;
            return current;
        }

        private SyntaxToken Match(TokenKind kind)
        {
            if (Current.Kind == kind)
                return NextToken();

            diagnostics.Add($"ERROR: Unexpected token <{Current.Kind}>, expected <{kind}>");
            return new SyntaxToken(kind, Current.Position, null, null);
        }

        private ExpressionSyntax ParsePrimaryExpression()
        {
            var numberToken = Match(TokenKind.NumberToken);

            return new NumberExpressionSyntax(numberToken);
        }

        public SyntaxTree Parse()
        {
            var expression = ParseExpression();
            var EOFToken = Match(TokenKind.EOFToken);
            return new SyntaxTree(diagnostics, expression, EOFToken);
        }

        public ExpressionSyntax ParseExpression()
        {
            var left = ParsePrimaryExpression();

            while (Current.Kind == TokenKind.PlusToken
                || Current.Kind == TokenKind.MinusToken)
            {
                var operatorToken = NextToken();
                var right = ParsePrimaryExpression();
                left = new BinaryExpressionSyntax(left, right, operatorToken);
            }

            return left;
        }
    }
}