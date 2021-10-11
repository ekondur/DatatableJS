using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DatatableJS
{
    [HtmlTargetElement("command-item", ParentTag = "columns")]
    public class CommandHelper : TagHelper
    {
        public string Field { get; set; }
        public string Title { get; set; }
        public string OnClick { get; set; }
        public string Text { get; set; }
        public string IconClass { get; set; }
        public string BtnClass { get; set; }
        public int Width { get; set; } = 1;

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
                Render = $@"'<a href=""#"" class=""{(!string.IsNullOrEmpty(IconClass) && string.IsNullOrEmpty(BtnClass) ? "btn btn-primary" : BtnClass)}"" onClick=""{OnClick}(\''+data+'\')""><i class=""{IconClass}""></i>'+{(string.IsNullOrEmpty(Text) ? (string.IsNullOrEmpty(IconClass) ? "data" : "''") : string.Format("' {0}'", Text))}+'</a>'"
            });
        }
    }
}
