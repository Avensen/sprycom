using System.Collections.Generic;

namespace Sprycom
{
    class Parser
    {
        public Parser(string text)
        {
            Lexer lexer = new Lexer(text);
            var tokens = new List<SyntaxToken>();
            SyntaxToken token;

            do
            {
                token = lexer.NextToken();
                if (token.Kind != SyntaxKind.WhitespaceToken) break;

            } while (token.Kind != SyntaxKind.EOFToken);
        }
    }
}