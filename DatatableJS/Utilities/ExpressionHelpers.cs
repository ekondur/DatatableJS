using System;
using System.Linq.Expressions;

namespace DatatableJS
{
    internal static class ExpressionHelpers<T>
    {
        public static string PropertyName<TProp>(Expression<Func<T, TProp>> expression)
        {
            var body = expression.Body as MemberExpression;

            if (body == null)
            {
                body = ((UnaryExpression)expression.Body).Operand as MemberExpression;
            }

            return body.Member.Name;
        }
    }
}
