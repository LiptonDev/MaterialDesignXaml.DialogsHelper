using System.Collections.Generic;

namespace MaterialDesignXaml.DialogsHelper
{
    /// <summary>
    /// Multiple identifiers.
    /// </summary>
    public interface IMultiDialogIdentifier
    {
        /// <summary>
        /// Identifiers.
        /// </summary>
        List<IDialogIdentifier> DialogIdentifiers { get; }
    }
}
