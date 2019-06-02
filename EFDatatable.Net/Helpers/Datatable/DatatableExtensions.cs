using EFDatatable.Models.Data;
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
                    var prop = typeof(T).GetProperty(request.columns[item.column].data);
                    if (item.dir == "asc")
                    {
                        query = query.OrderBy(prop.Name);
                    }
                    else
                    {
                        query = query.OrderByDescending(prop.Name);
                    }
                }
            }
            result.data = query.ToList();
            return result;
        }

        private static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, string memberName)
        {
            var typeParams = new ParameterExpression[] { Expression.Parameter(typeof(T), "") };
            var pi = typeof(T).GetProperty(memberName);
            return (IOrderedQueryable<T>)query.Provider.CreateQuery(
                Expression.Call(
                    typeof(Queryable),
                    "OrderBy",
                    new Type[] { typeof(T), pi.PropertyType },
                    query.Expression,
                    Expression.Lambda(Expression.Property(typeParams[0], pi), typeParams))
            );
        }

        private static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> query, string memberName)
        {
            var typeParams = new ParameterExpression[] { Expression.Parameter(typeof(T), "") };
            var pi = typeof(T).GetProperty(memberName);
            return (IOrderedQueryable<T>)query.Provider.CreateQuery(
                Expression.Call(
                    typeof(Queryable),
                    "OrderByDescending",
                    new Type[] { typeof(T), pi.PropertyType },
                    query.Expression,
                    Expression.Lambda(Expression.Property(typeParams[0], pi), typeParams))
            );
        }
    }
}