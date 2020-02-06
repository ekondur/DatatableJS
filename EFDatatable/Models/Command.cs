namespace EFDatatable
{
    /// <summary>
    /// Define a command.
    /// </summary>
    public class Command
    {
        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; private set; }
        /// <summary>
        /// Gets the on click.
        /// </summary>
        /// <value>
        /// The on click.
        /// </value>
        public string OnClick { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="onClick">The on click.</param>
        public Command(string title, string onClick)
        {
            Title = title;
            OnClick = onClick;
        }
    }
}
