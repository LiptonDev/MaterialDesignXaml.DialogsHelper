using MaterialDesignThemes.Wpf;
using MaterialDesignXaml.DialogsHelper.Dialogs;
using MaterialDesignXaml.DialogsHelper.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialDesignXaml.DialogsHelper
{
    public static class DialogHelper
    {
        /// <summary>
        /// All opened dialogs.
        /// </summary>
        static Dictionary<string, DialogSession> Sessions = new Dictionary<string, DialogSession>();

        static async Task<object> BaseShowAsync(this IDialogIdentifier identifier, object content, Action openedEvent, Action closedEvent) =>
            await DialogHost.Show(content, identifier.Identifier, (_, e) =>
            {
                Sessions.Add(identifier.Identifier, e.Session);
                openedEvent?.Invoke();
            }, (_, e) =>
            {
                Sessions.Remove(identifier.Identifier);
                closedEvent?.Invoke();
            });

        /// <summary>
        /// Show message box.
        /// </summary>
        /// <param name="identifier">Dialog identifier.</param>
        /// <param name="content">Dialog content.</param>
        /// <param name="buttons">Buttons.</param>
        /// <returns></returns>
        public static async Task<MaterialMessageBoxButtons> ShowMessageBoxAsync(this IDialogIdentifier identifier, object content, MaterialMessageBoxButtons buttons) =>
            await identifier.ShowAsync<MaterialMessageBoxButtons>(new MaterialMessageBox(content, buttons));

        /// <summary>
        /// Update dialog content.
        /// </summary>
        /// <param name="identifier">Dialog identifier.</param>
        /// <param name="content">New content.</param>
        public static void UpdateContent(this IDialogIdentifier identifier, object content)
        {
            if (!Sessions.ContainsKey(identifier.Identifier))
                return;

            Sessions[identifier.Identifier].UpdateContent(content);
        }

        #region Show methods
        /// <summary>
        /// Show dialog with content.
        /// </summary>
        /// <param name="identifier">Dialog identifier.</param>
        /// <param name="content">Dialog content/</param>
        /// <param name="openedEvent">Raised when dialog showed.</param>
        /// <param name="closedEvent">Raised when dialog closed.</param>
        /// <returns></returns>
        public static async Task<T> ShowAsync<T>(this IDialogIdentifier identifier, object content, Action openedEvent, Action closedEvent) =>
            (T)await identifier.BaseShowAsync(content, openedEvent, closedEvent);


        /// <summary>
        /// Show dialog with content.
        /// </summary>
        /// <param name="identifier">Dialog identifier.</param>
        /// <param name="content">Dialog content/</param>
        /// <param name="openedEvent">Raised when dialog showed.</param>
        /// <returns></returns>
        public static async Task<T> ShowAsync<T>(this IDialogIdentifier identifier, object content, Action openedEvent) =>
            (T)await identifier.BaseShowAsync(content, openedEvent, null);

        /// <summary>
        /// Show dialog with content.
        /// </summary>
        /// <param name="identifier">Dialog identifier.</param>
        /// <param name="content">Dialog content/</param>
        /// <param name="openedEvent">Raised when dialog showed.</param>
        /// <param name="closedEvent">Raised when dialog closed.</param>
        /// <returns></returns>
        public static async Task<object> ShowAsync(this IDialogIdentifier identifier, object content, Action openedEvent, Action closedEvent) =>
            await identifier.BaseShowAsync(content, openedEvent, closedEvent);


        /// <summary>
        /// Show dialog with content.
        /// </summary>
        /// <param name="identifier">Dialog identifier.</param>
        /// <param name="content">Dialog content/</param>
        /// <param name="openedEvent">Raised when dialog showed.</param>
        /// <returns></returns>
        public static async Task<object> ShowAsync(this IDialogIdentifier identifier, object content, Action openedEvent) =>
            await identifier.BaseShowAsync(content, openedEvent, null);

        /// <summary>
        /// Show dialog with content.
        /// </summary>
        /// <param name="identifier">Dialog identifier.</param>
        /// <param name="content">Dialog content/</param>
        /// <returns></returns>
        public static async Task<object> ShowAsync(this IDialogIdentifier identifier, object content) =>
            await identifier.BaseShowAsync(content, null, null);

        /// <summary>
        /// Show dialog with content.
        /// </summary>
        /// <param name="identifier">Dialog identifier.</param>
        /// <param name="content">Dialog content/</param>
        /// <returns></returns>
        public static async Task<T> ShowAsync<T>(this IDialogIdentifier identifier, object content) =>
            (T)await identifier.ShowAsync(content);
        #endregion

        #region Close methods
        /// <summary>
        /// Close dialog with parameter.
        /// </summary>
        /// <param name="dialog">Closable dialog.</param>
        /// <param name="parameter">Parameter.</param>
        public static void Close(this IClosableDialog dialog, object parameter) =>
            dialog.Identifier.Close(parameter);

        /// <summary>
        /// Close dialog.
        /// </summary>
        /// <param name="dialog">Closable dialog.</param>
        public static void Close(this IClosableDialog dialog) =>
            dialog.Identifier.Close();

        /// <summary>
        /// Close dialog with parameter.
        /// </summary>
        /// <param name="identifier">Dialog identifier.</param>
        /// <param name="parameter">Parameter.</param>
        public static void Close(this IDialogIdentifier identifier, object parameter)
        {
            if (!Sessions.ContainsKey(identifier.Identifier))
                return;

            Sessions[identifier.Identifier].Close(parameter);
        }

        /// <summary>
        /// Close dialog.
        /// </summary>
        /// <param name="identifier">Dialog identifier.</param>
        public static void Close(this IDialogIdentifier identifier) => identifier.Close(null);

        /// <summary>
        /// Closing all opened dialogs.
        /// </summary>
        public static void CloseAll()
        {
            foreach (var item in Sessions.Values.ToArray())
                item.Close(null);
        }
        #endregion

        #region Old methods
        /// <summary>
        /// Show dialog with content.
        /// </summary>
        /// <param name="identifier">Dialog identifier.</param>
        /// <param name="content">Dialog content/</param>
        /// <returns></returns>
        [Obsolete("Use ShowAsync method", true)]
        public static async Task<object> Show(this IDialogIdentifier identifier, object content) =>
            await Task.FromResult<object>(null);

        /// <summary>
        /// Show dialog with content.
        /// </summary>
        /// <param name="identifier">Dialog identifier.</param>
        /// <param name="content">Dialog content/</param>
        /// <returns></returns>
        [Obsolete("Use ShowAsync method", true)]
        public static async Task<T> Show<T>(this IDialogIdentifier identifier, object content) =>
            await Task.FromResult(default(T));
        #endregion
    }
}
