using System.Collections.Generic;

namespace DatatableJS
{
    public class LengthMenuModel
    {
        internal List<int> _lengthMenuValues { get; set; } = new List<int>();
        internal List<string> _lengthMenuDisplayedTexts { get; set; } = new List<string>();
        internal int? _pageLength { get; set; }
    }
}
