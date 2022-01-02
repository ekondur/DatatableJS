using DatatableJS.Exceptions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;

namespace DatatableJS.Helpers
{
    /// <summary>
    /// Enable ordering and set default orders.
    /// </summary>
    [HtmlTargetElement("orders", ParentTag = "js-datatable")]
    public class OrdersHelper : TagHelper
    {
        /// <summary>
        /// Process
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
        }
    }

    /// <summary>
    /// Adds the specified property for default ordering.
    /// </summary>
    [HtmlTargetElement("add", ParentTag = "orders")]
    public class OrderHelper : TagHelper
    {
        public string Field { get; set; }
        public OrderBy OrderBy { get; set; }

        /// <summary>
        /// Process
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.Clear();
            var grid = CommonHelpers.GetGrid(context);
            grid.Ordering = true;

            var column = grid.Columns.Where(c => c.Data.Equals(Field)).FirstOrDefault();

            if (column == null)
            {
                throw new ColumnNotFoundException(string.Format("Column not found for property {0}", Field));
            }

            var columnIndex = grid.Columns.IndexOf(column);

            grid.Orders.Add(new OrderModel
            {
                Field = Field,
                Column = columnIndex,
                OrderBy = OrderBy
            });
        }
    }
}
