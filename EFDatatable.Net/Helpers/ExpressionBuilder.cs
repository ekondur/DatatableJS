using EFDatatable.Models.Definitions;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace EFDatatable.Net.Helpers
{
    public class ExpressionBuilder
    {
        private static MethodInfo containsMethod = typeof(string).GetMethod("Contains");
        private static MethodInfo startsWithMethod =
        typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
        private static MethodInfo endsWithMethod =
        typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });


        public static Expression<Func<T, bool>> GetExpression<T>(FilterDefinition filter)
        {
            if (filter == null) return null;

            ParameterExpression param = Expression.Parameter(typeof(T), "t");
            var exp = GetExpression<T>(param, filter);
            return Expression.Lambda<Func<T, bool>>(exp, param);
        }

        private static Expression GetExpression<T>(ParameterExpression param, FilterDefinition filter)
        {
            MemberExpression member = Expression.Property(param, filter.Field);
            var cType = ConvertDynamic(Type.GetTypeCode(member.Type), filter.Value);          
            ConstantExpression constant = Expression.Constant(cType);
            
            switch (filter.Operand)
            {
                case Operand.Equals:
                    return Expression.Equal(member, constant);

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
            }
            return null;
        }

        private static object ConvertDynamic(TypeCode code, string value)
        {
            switch (code)
            {
                case TypeCode.Empty:
                    return string.Empty;
                case TypeCode.Object:
                    return value;
                case TypeCode.Boolean:
                    return Convert.ToBoolean(value);
                case TypeCode.Char:
                    return Convert.ToChar(value);
                case TypeCode.SByte:
                    return Convert.ToSByte(value);
                case TypeCode.Byte:
                    return Convert.ToByte(value);
                case TypeCode.Int16:
                    return Convert.ToInt16(value);
                case TypeCode.UInt16:
                    return Convert.ToUInt16(value);
                case TypeCode.Int32:
                    return Convert.ToInt32(value);
                case TypeCode.UInt32:
                    return Convert.ToUInt32(value);
                case TypeCode.Int64:
                    return Convert.ToInt64(value);
                case TypeCode.UInt64:
                    return Convert.ToUInt64(value);
                case TypeCode.Single:
                    return Convert.ToSingle(value);
                case TypeCode.Double:
                    return Convert.ToDouble(value);
                case TypeCode.Decimal:
                    return Convert.ToDecimal(value);
                case TypeCode.DateTime:
                    return Convert.ToDateTime(value);
                case TypeCode.String:
                    return value;
                default:
                    return null;
            }
        }
    }
}