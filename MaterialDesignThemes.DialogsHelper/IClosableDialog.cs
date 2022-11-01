using System;
using System.Windows.Input;

namespace MaterialDesignXaml.DialogsHelper
{
    /// <summary>
    /// Closable dialog.
    /// </summary>
    public interface IClosableDialog
    {
        /// <summary>
        /// Command for closing dialog.
        /// </summary>
        ICommand CloseDialogCommand { get; }

        /// <summary>
        /// DialogHost owner.
        /// </summary>
        IDialogIdentifier OwnerIdentifier { get; }
    }
}
