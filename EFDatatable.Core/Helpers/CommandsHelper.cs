using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFDatatable.Core.Helpers
{
    [HtmlTargetElement("commands", ParentTag = "columns")]
    public class CommandsHelper : TagHelper
    {
        public string Field { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string BtnClass { get; set; } = "btn btn-secondary";
        public int Width { get; set; } = 1;
        public string BtnId { get; set; } = "btn-group";

        public IEnumerable<Command> Items { get; set; } = new List<Command>();

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "th";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Content.Append(Title ?? "#");

            var grid = CommonHelpers.GetGrid(context);
            grid.Columns.Add(new ColumnModel
            {
                Data = Field,
                Visible = true,
                Searchable = false,
                Orderable = false,
                Width = Width,
                Render = $@"'<div class=""btn-group"" role=""group"">'+
                        '<button id=""{BtnId}"" type=""button"" class=""{BtnClass} dropdown-toggle"" data-toggle=""dropdown"" aria-haspopup=""true"" aria-expanded=""false"">' +
                            '{Text ?? ""}'+
                        '</button>'+
                        '<div class=""dropdown-menu"" aria-labelledby=""{BtnId}"">'+
                        {
                    string.Join(Environment.NewLine,
                        Items.Select(a => $@"'<a class=""dropdown-item"" href=""#"" onclick=""{a.OnClick}('+data+');return false;"">{a.Text}</a>'+"))}
                        '</div>'+
                    '</div>'"
            });
        }
    }
}
