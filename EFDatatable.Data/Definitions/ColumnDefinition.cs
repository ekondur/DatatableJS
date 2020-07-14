using System;
using System.Reflection;

namespace EFDatatable.Data
{
    public class ColumnDefinition
    {
        public string Data { get; set; }
        public string Title { get; set; }
        public bool Visible { get; set; } = true;
        public bool Searchable { get; set; } = true;
        public bool Orderable { get; set; } = true;
        public int Width { get; set; }
        public string ClassName { get; set; } = "";
        public string Render { get; set; } = "data";
        public Type Type { get; set; }
    }
}
