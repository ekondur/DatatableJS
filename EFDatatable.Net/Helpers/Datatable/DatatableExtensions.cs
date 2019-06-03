using EFDatatable.Models.Data;
using EFDatatable.Models.Definitions;
using EFDatatable.Net.Helpers;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace EFDatatable.Net
{
    public static class DatatableExtensions
    {
        public static DataResult<T> ToDataResult<T>(this IQueryable<T> query, DataRequest request) where T : class
        {
            var result = new DataResult<T>
            {
                draw = request.draw
            };
            result.recordsTotal = result.recordsFiltered = query.Count();
            if (request.draw > 0)
            {
                if (request.start > 0)
                {
                    query = query.Skip(request.start);
                }
                query = query.Take(request.length);

                foreach (var item in request.order)
                {
                    if (item.dir == "asc")
                    {
                        query = query.OrderBy(request.columns[item.column].data);
                    }
                    else
                    {
                        query = query.OrderByDescending(request.columns[item.column].data);
                    }
                }

                foreach (var item in request.filters)
                {
                    var exp = ExpressionBuilder
                        .GetExpression<T>(new FilterDefinition
                        {
                            Operand = (Operand)item.Operand,
                            Field = item.Field,
                            Value = item.Value
                        });
                    query = query.Where(exp);
                }

                if (!string.IsNullOrEmpty(request.search.value))
                {
                }
            }
            result.data = query.ToList();
            return result;
        }

        private static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, string memberName)
        {
            return OrderByCreate(query, memberName, "OrderBy");
        }

        private static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> query, string memberName)
        {
            return OrderByCreate(query, memberName, "OrderByDescending");
        }

        private static IOrderedQueryable<T> OrderByCreate<T>(this IQueryable<T> query, string memberName, string direction)
        {
            var typeParams = new ParameterExpression[] { Expression.Parameter(typeof(T), "") };
            var pi = typeof(T).GetProperty(memberName);
            return (IOrderedQueryable<T>)query.Provider.CreateQuery(
                Expression.Call(
                    typeof(Queryable),
                    direction,
                    new Type[] { typeof(T), pi.PropertyType },
                    query.Expression,
                    Expression.Lambda(Expression.Property(typeParams[0], pi), typeParams))
            );
        }
    }
}