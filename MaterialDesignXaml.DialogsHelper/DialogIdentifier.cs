namespace MaterialDesignXaml.DialogsHelper
{
    /// <summary>
    /// Implementation for IDialogIdentifier.
    /// </summary>
    public class DialogIdentifier : IDialogIdentifier
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="identifier">Dialog identifier.</param>
        public DialogIdentifier(string identifier) => Identifier = identifier;

        /// <summary>
        /// Dialog identifier.
        /// </summary>
        public string Identifier { get; }
    }
}
