using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DatatableJS.Data
{
    /// <summary>
    /// Includes ToDataResult extension
    /// </summary>
    public static class DatatableExtensions
    {
        /// <summary>
        /// Executes IQueryable list and returns DataResult includes data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="request"></param>
        /// <returns>DataResult</returns>
        public static DataResult<T> ToDataResult<T>(this IQueryable<T> query, DataRequest request) where T : class
        {
            var result = new DataResult<T>
            {
                draw = request.draw
            };
            result.recordsTotal = result.recordsFiltered = query.Count();

            foreach (var item in request.filters)
            {
                var exp = GetExpression<T>(item.Operand, item.Field, item.Value);
                if (exp != null)
                {
                    query = query.Where(exp);
                }
            }

            var listExp = new List<FilterDef>();

            if (!string.IsNullOrEmpty(request.search?.value))
            {
                foreach (var item in request.columns.Where(a => a.searchable))
                {
                    ParameterExpression param = Expression.Parameter(typeof(T), "t");
                    MemberExpression member = Expression.Property(param, item.data);
                    var operand = member.Type == typeof(string) ? Operand.Contains : Operand.Equal;
                    listExp.Add(new FilterDef { Operand = operand, Field = item.data, Value = request.search.value, Operator = Operator.Or });
                }
            }

            foreach (var item in request.columns.Where(a => a.searchable == true && !string.IsNullOrEmpty(a.search.value)))
            {
                ParameterExpression param = Expression.Parameter(typeof(T), "t");
                MemberExpression member = Expression.Property(param, item.data);
                var operand = member.Type == typeof(string) ? Operand.Contains : Operand.Equal;
                listExp.Add(new FilterDef { Operand = operand, Field = item.data, Value = item.search.value, Operator = Operator.And });
            }

            if (listExp.Any())
            {
                Expression<Func<T, bool>> exp = null;
                exp = ExpressionBuilder.GetExpression<T>(listExp);
                if (exp != null) query = query.Where(exp);
            }

            if (listExp.Any() || request.filters.Any())
            {
                result.recordsFiltered = query.Count();
            }

            if (request.draw > 0)
            {
                if (!request.order.Any())
                {
                    query = query.OrderBy(request.columns[0].data ?? request.columns[1].data);
                }
                else
                {
                    query = request.order[0].dir != "asc"
                        ? (IQueryable<T>)query.OrderByDescending<T>(request.columns[request.order[0].column].data)
                        : (IQueryable<T>)query.OrderBy<T>(request.columns[request.order[0].column].data);

                    for (var i = 1; i < request.order.Count(); i++)
                    {
                        query = request.order[i].dir != "asc"
                            ? (IQueryable<T>)query.ThenByDescending<T>(request.columns[request.order[i].column].data)
                            : (IQueryable<T>)query.ThenBy<T>(request.columns[request.order[i].column].data);
                    }
                }

                if (request.length != -1)
                {
                    query = query.Skip(request.start).Take(request.length);
                }
            }

            result.data = query.ToList();
            return result;
        }

        private static Expression<Func<T, bool>> GetExpression<T>(Operand operand, string field, string value)
        {
            return ExpressionBuilder
                .GetExpression<T>(new FilterDef
                {
                    Operand = operand,
                    Field = field,
                    Value = value
                });
        }

        private static IOrderedQueryable<T> OrderBy<T>(
            this IQueryable<T> query,
            string memberName)
        {
            return query.OrderByCreate<T>(memberName, nameof(OrderBy));
        }

        private static IOrderedQueryable<T> ThenBy<T>(
            this IQueryable<T> query,
            string memberName)
        {
            return query.OrderByCreate<T>(memberName, nameof(ThenBy));
        }

        private static IOrderedQueryable<T> OrderByDescending<T>(
            this IQueryable<T> query,
            string memberName)
        {
            return query.OrderByCreate<T>(memberName, nameof(OrderByDescending));
        }

        private static IOrderedQueryable<T> ThenByDescending<T>(
            this IQueryable<T> query,
            string memberName)
        {
            return query.OrderByCreate<T>(memberName, nameof(ThenByDescending));
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
