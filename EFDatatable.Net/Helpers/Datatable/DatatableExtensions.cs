using EFDatatable.Models.Data;
using System.Linq;

namespace EFDatatable.Net
{
    public static class DatatableExtensions
    {
        public static DataResult<T> ToDataResult<T>(this IQueryable<T> query, DataRequest request)
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
            }
            result.data = query.ToList();
            return result;
        }
    }
}