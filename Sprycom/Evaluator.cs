using System;

namespace Sprycom
{
    class Evaluator
    {
        private readonly ExpressionSyntax root;

        public Evaluator(ExpressionSyntax root)
        {
            this.root = root;
        }

        public int Evaluate()
        {
            return EvaluateExpression(root);
        }

        private int EvaluateExpression(ExpressionSyntax node)
        {
            if (node is NumberExpressionSyntax n)
                return (int)n.NumberToken.Value;
            if (node is BinaryExpressionSyntax b)
            {
                var left = EvaluateExpression(b.Left);
                var right = EvaluateExpression(b.Right);

                if (b.OperatorToken.Kind == TokenKind.PlusToken)
                    return left + right;
                if (b.OperatorToken.Kind == TokenKind.MinusToken)
                    return left - right;
                if (b.OperatorToken.Kind == TokenKind.StarToken)
                    return left * right;
                if (b.OperatorToken.Kind == TokenKind.SlashToken)
                    return left / right;
                
                throw new Exception("Unexpected BO");
            }
            if (node is BracketedExpressionSyntax be)
                return EvaluateExpression(be.Expression);

            throw new Exception("Unexpected NE");
        }
    }
}
