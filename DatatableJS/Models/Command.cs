namespace DatatableJS 
{ 
    public class Command
    {
        public string Text { get; private set; }
        public string OnClick { get; private set; }
        public Command(string text, string onClick)
        {
            Text = text;
            OnClick = onClick;
        }
    }
}
