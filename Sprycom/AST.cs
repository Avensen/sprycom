using System.Collections.Generic;

namespace Sprycom
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
}