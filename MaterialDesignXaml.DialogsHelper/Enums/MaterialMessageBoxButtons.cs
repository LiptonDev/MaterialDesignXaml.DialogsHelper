using System;

namespace MaterialDesignXaml.DialogsHelper.Enums
{
    /// <summary>
    /// Buttons.
    /// </summary>
    [Flags]
    public enum MaterialMessageBoxButtons : byte
    {
        /// <summary>
        /// OK button.
        /// </summary>
        Ok = 1,

        /// <summary>
        /// CANCEL button.
        /// </summary>
        Cancel = 2,

        /// <summary>
        /// YES button.
        /// </summary>
        Yes = 4,

        /// <summary>
        /// NO button.
        /// </summary>
        No = 8,

        /// <summary>
        /// YES and NO buttons.
        /// </summary>
        YesNo = Yes | No,

        /// <summary>
        /// OK and CANCEL buttons.
        /// </summary>
        OkCancel = Ok | Cancel,

        /// <summary>
        /// All buttons
        /// </summary>
        All = Ok | Cancel | Yes | No
    }
}