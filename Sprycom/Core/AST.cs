using System.Collections.Generic;

namespace Sprycom.Core
{
    abstract class SyntaxNode
    {
        public abstract TokenKind Kind { get; }

        public abstract IEnumerable<SyntaxNode> GetChildren();
    }

    abstract class ExpressionSyntax : SyntaxNode
    {

    }

    sealed class NumberExpressionSyntax : ExpressionSyntax
    {
        public NumberExpressionSyntax(SyntaxToken numberToken)
        {
            NumberToken = numberToken;
        }

        public SyntaxToken NumberToken { get; }
        public override TokenKind Kind => TokenKind.NumberExpression;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return NumberToken;
        }
    }

    sealed class BinaryExpressionSyntax : ExpressionSyntax
    {
        public BinaryExpressionSyntax(ExpressionSyntax left, ExpressionSyntax right, SyntaxToken operatorToken)
        {
            Left = left;
            Right = right;
            OperatorToken = operatorToken;
        }

        public ExpressionSyntax Left { get; }
        public ExpressionSyntax Right { get; }
        public SyntaxToken OperatorToken { get; }

        public override TokenKind Kind => TokenKind.BinaryExpresion;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return Left;
            yield return OperatorToken;
            yield return Right;
        }
    }

    sealed class BracketedExpressionSyntax : ExpressionSyntax
    {
        public BracketedExpressionSyntax(SyntaxToken openBracketToken, SyntaxToken cosedBracketToken, ExpressionSyntax expression)
        {
            OpenBracketToken = openBracketToken;
            ClosedBracketToken = cosedBracketToken;
            Expression = expression;
        }

        public override TokenKind Kind => TokenKind.BracketedExpression;

        public SyntaxToken OpenBracketToken { get; }
        public SyntaxToken ClosedBracketToken { get; }
        public ExpressionSyntax Expression { get; }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return OpenBracketToken;
            yield return Expression;
            yield return ClosedBracketToken;
        }
    }
}