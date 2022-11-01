namespace MaterialDesignXaml.DialogsHelper.Models
{
    /// <summary>
    /// Message box button.
    /// </summary>
    public class MaterialMessageBoxButton
    {
        /// <summary>
        /// Button content.
        /// </summary>
        public object Content { get; }

        /// <summary>
        /// Button type.
        /// </summary>
        public object ReturnValue { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public MaterialMessageBoxButton(object content, object returnValue)
        {
            Content = content;
            ReturnValue = returnValue;
        }
    }
}
