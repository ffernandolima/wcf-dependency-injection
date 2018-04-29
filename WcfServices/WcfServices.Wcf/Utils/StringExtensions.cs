using System;
using System.Linq.Expressions;

namespace WcfServices.Wcf.Utils
{
	internal static class StringExtensions
	{
		public static string Of<T>(Expression<Func<T>> expression)
		{
			return GetMemberName(expression.Body);
		}

		public static string Of(Expression<Action> expression)
		{
			return GetMemberName(expression.Body);
		}

		public static string Of<T>()
		{
			return typeof(T).Name;
		}

		private static string GetMemberName(Expression expression)
		{
			if (expression == null)
			{
				return string.Empty;
			}

			switch (expression.NodeType)
			{
				case ExpressionType.MemberAccess:
					var memberExpression = (MemberExpression)expression;
					var supername = GetMemberName(memberExpression.Expression);

					return string.IsNullOrEmpty(supername) ? memberExpression.Member.Name : string.Concat(supername, '.', memberExpression.Member.Name);

				case ExpressionType.Call:
					var callExpression = (MethodCallExpression)expression;
					return callExpression.Method.Name;

				case ExpressionType.Convert:
					var unaryExpression = (UnaryExpression)expression;
					return GetMemberName(unaryExpression.Operand);

				case ExpressionType.Constant:
				case ExpressionType.Parameter:
					return string.Empty;

				default:
					throw new ArgumentException("The expression is not a member access or method call expression");
			}
		}
	}
}
