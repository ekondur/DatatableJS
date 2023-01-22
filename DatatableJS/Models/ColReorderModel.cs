namespace DatatableJS
{
    class ColReorderModel
    {
        public bool ColReorder { get; set; }
        public bool Settings { get; set; }
        public bool Enable { get; set; } = true;
        public int FixedColumnsLeft { get; set; }
        public int FixedColumnsRight { get; set; }
        public string Order { get; set; } = "null";
        public bool RealTime { get; set; } = true;
    }
}
