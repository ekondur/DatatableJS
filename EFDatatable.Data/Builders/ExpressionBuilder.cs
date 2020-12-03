using System;
using System.Collections.Generic;
using System.ComponentModel;
#if NETFRAMEWORK
using System.Data.Entity;
#endif
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace EFDatatable.Data
{
    public static class ExpressionBuilder
    {
        private static readonly MethodInfo containsMethod = typeof(string).GetMethod("Contains");
        private static readonly MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
        private static readonly MethodInfo endsWithMethod = typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });


        public static Expression<Func<T, bool>> GetExpression<T>(FilterDefinition filter)
        {
            if (filter == null)
            {
                return null;
            }

            ParameterExpression param = Expression.Parameter(typeof(T), "t");
            var exp = GetExpression(param, filter);
            if (exp == null)
            {
                return null;
            }
            return Expression.Lambda<Func<T, bool>>(exp, param);
        }

        private static Expression GetExpression(ParameterExpression param, FilterDefinition filter)
        {
            MemberExpression member = Expression.Property(param, filter.Field);
            var converter = TypeDescriptor.GetConverter(member.Type);
            if (!converter.IsValid(filter.Value))
            {
                return null;
            }
            var propertyValue = converter.ConvertFromInvariantString(filter.Value);
            ConstantExpression constant = Expression.Constant(propertyValue, member.Type);

            switch (filter.Operand)
            {
                case Operand.Equal:
#if NETFRAMEWORK
                    if (member.Type == typeof(DateTime?) || member.Type == typeof(DateTime))
                    {
                        var me = Expression.Convert(member, member.Type);
                        var ex = Expression.Call(null, typeof(DbFunctions).GetMethod("TruncateTime", new Type[] { member.Type }), me);
                        return Expression.Equal(ex, constant);
                    }
#endif
                    return Expression.Equal(member, constant);

                case Operand.NotEqual:
                    return Expression.NotEqual(member, constant);

                case Operand.GreaterThan:
                    return Expression.GreaterThan(member, constant);

                case Operand.GreaterThanOrEqual:
                    return Expression.GreaterThanOrEqual(member, constant);

                case Operand.LessThan:
                    return Expression.LessThan(member, constant);

                case Operand.LessThanOrEqual:
                    return Expression.LessThanOrEqual(member, constant);

                case Operand.Contains:
                    return Expression.Call(member, containsMethod, constant);

                case Operand.StartsWith:
                    return Expression.Call(member, startsWithMethod, constant);

                case Operand.EndsWith:
                    return Expression.Call(member, endsWithMethod, constant);
                default:
                    return null;
            }
        }

        public static Expression<Func<T, bool>> GetExpression<T>(IList<FilterDefinition> filters)
        {
            if (filters.Count == 0)
            {
                return null;
            }

            ParameterExpression param = Expression.Parameter(typeof(T), "t");
            Expression exp = null;

            foreach (var filter in filters)
            {
                var isAnd = filter.Operator == Operator.And;
                var expin = GetExpression(param, filter);
                if (expin != null)
                {
                    if (exp == null)
                    {
                        exp = expin;
                    }
                    else
                    {
                        exp = isAnd ? Expression.And(exp, expin) : Expression.Or(exp, expin);
                    }
                }
                else
                {
                    if (exp != null)
                    {
                        exp = isAnd ? Expression.And(exp, Expression.Constant(false)) : Expression.Or(exp, Expression.Constant(false));
                    }
                    else
                    {
                        exp = isAnd ? Expression.And(Expression.Constant(false), Expression.Constant(false)) : Expression.Or(Expression.Constant(false), Expression.Constant(false));
                    }
                }
            }

            return Expression.Lambda<Func<T, bool>>(exp, param);
        }
    }
}
