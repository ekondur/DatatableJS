namespace EFDatatable
{
    /// <summary>
    /// Define a command.
    /// </summary>
    public class Command
    {
        public string Title { get; private set; }
        public string OnClick { get; private set; }
        public Command(string title, string onClick)
        {
            Title = title;
            OnClick = onClick;
        }
    }
}
