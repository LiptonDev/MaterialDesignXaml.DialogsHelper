using MaterialDesignThemes.Wpf;
using MaterialDesignXaml.DialogsHelper.Dialogs;
using MaterialDesignXaml.DialogsHelper.Models;
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
        static readonly Dictionary<string, DialogSession> Sessions = new Dictionary<string, DialogSession>();

        static async Task<object?> BaseShowAsync(this IDialogIdentifier identifier, object content, Action openedEvent, Action closedEvent)
        {
            return await DialogHost.Show(content, identifier.Identifier, Open, Close);

            void Open(object sender, DialogOpenedEventArgs eventArgs)
            {
                Sessions.Add(identifier.Identifier, eventArgs.Session);
                openedEvent?.Invoke();
            }

            void Close(object sender, DialogClosingEventArgs eventArgs)
            {
                Sessions.Remove(identifier.Identifier);
                closedEvent?.Invoke();
            }
        }

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

        #region Message box
        /// <summary>
        /// Show message box.
        /// </summary>
        /// <param name="identifier">Dialog identifier.</param>
        /// <param name="content">Dialog content.</param>
        /// <param name="buttons">Buttons.</param>
        /// <returns></returns>
        public static async Task<object?> ShowMessageBoxAsync(this IDialogIdentifier identifier, object content, params MaterialMessageBoxButton[] buttons)
        {
            return await identifier.ShowMessageBoxAsync(content, null, null, buttons);
        }

        /// <summary>
        /// Show message box.
        /// </summary>
        /// <param name="identifier">Dialog identifier.</param>
        /// <param name="content">Dialog content.</param>
        /// <param name="buttons">Buttons.</param>
        /// <returns></returns>
        public static async Task<object?> ShowMessageBoxAsync(this IDialogIdentifier identifier,
                                                                   object content,
                                                                   Action openedEvent = null,
                                                                   Action closedEvent = null,
                                                                   params MaterialMessageBoxButton[] buttons)
        {
            return await identifier.ShowAsync<object?>(new MaterialMessageBox(content, buttons), openedEvent, closedEvent);
        }

        /// <summary>
        /// Show message box.
        /// </summary>
        /// <param name="identifier">Dialog identifier.</param>
        /// <param name="content">Dialog content.</param>
        /// <param name="buttons">Buttons.</param>
        /// <returns></returns>
        public static async Task<object?> ShowMessageBoxAsync(this IDialogIdentifier identifier,
                                                                   object content,
                                                                   IEnumerable<MaterialMessageBoxButton> buttons,
                                                                   Action openedEvent = null,
                                                                   Action closedEvent = null)
        {
            return await identifier.ShowAsync<object?>(new MaterialMessageBox(content, buttons), openedEvent, closedEvent);
        }
        #endregion

        #region Show methods
        /// <summary>
        /// Show dialog with content.
        /// </summary>
        /// <param name="identifier">Dialog identifier.</param>
        /// <param name="content">Dialog content/</param>
        /// <param name="openedEvent">Raised when dialog showed.</param>
        /// <param name="closedEvent">Raised when dialog closed.</param>
        /// <returns></returns>
        public static async Task<T> ShowAsync<T>(this IDialogIdentifier identifier, object content, Action openedEvent, Action closedEvent)
        {
            return (T)await identifier.BaseShowAsync(content, openedEvent, closedEvent);
        }


        /// <summary>
        /// Show dialog with content.
        /// </summary>
        /// <param name="identifier">Dialog identifier.</param>
        /// <param name="content">Dialog content/</param>
        /// <param name="openedEvent">Raised when dialog showed.</param>
        /// <returns></returns>
        public static async Task<T> ShowAsync<T>(this IDialogIdentifier identifier, object content, Action openedEvent)
        {
            return (T)await identifier.BaseShowAsync(content, openedEvent, null);
        }

        /// <summary>
        /// Show dialog with content.
        /// </summary>
        /// <param name="identifier">Dialog identifier.</param>
        /// <param name="content">Dialog content/</param>
        /// <param name="openedEvent">Raised when dialog showed.</param>
        /// <param name="closedEvent">Raised when dialog closed.</param>
        /// <returns></returns>
        public static async Task<object?> ShowAsync(this IDialogIdentifier identifier, object content, Action openedEvent, Action closedEvent)
        {
            return await identifier.BaseShowAsync(content, openedEvent, closedEvent);
        }


        /// <summary>
        /// Show dialog with content.
        /// </summary>
        /// <param name="identifier">Dialog identifier.</param>
        /// <param name="content">Dialog content/</param>
        /// <param name="openedEvent">Raised when dialog showed.</param>
        /// <returns></returns>
        public static async Task<object?> ShowAsync(this IDialogIdentifier identifier, object content, Action openedEvent)
        {
            return await identifier.BaseShowAsync(content, openedEvent, null);
        }

        /// <summary>
        /// Show dialog with content.
        /// </summary>
        /// <param name="identifier">Dialog identifier.</param>
        /// <param name="content">Dialog content/</param>
        /// <returns></returns>
        public static async Task<object?> ShowAsync(this IDialogIdentifier identifier, object content)
        {
            return await identifier.BaseShowAsync(content, null, null);
        }

        /// <summary>
        /// Show dialog with content.
        /// </summary>
        /// <param name="identifier">Dialog identifier.</param>
        /// <param name="content">Dialog content/</param>
        /// <returns></returns>
        public static async Task<T> ShowAsync<T>(this IDialogIdentifier identifier, object content)
        {
            return (T)await identifier.ShowAsync(content);
        }
        #endregion

        #region Close methods
        /// <summary>
        /// Close all dialogs with parameter.
        /// </summary>
        /// <param name="multiDialogIdentifier">Dialogs identifiers.</param>
        /// <param name="parameter">Parameter.</param>
        public static void CloseAll(this IMultiDialogIdentifier multiDialogIdentifier, object parameter)
        {
            multiDialogIdentifier.DialogIdentifiers.ForEach(x => x.Close(parameter));
        }

        /// <summary>
        /// Close all dialogs.
        /// </summary>
        /// <param name="multiDialogIdentifier">Dialogs identifiers.</param>
        public static void CloseAll(this IMultiDialogIdentifier multiDialogIdentifier)
        {
            multiDialogIdentifier.CloseAll(null);
        }


        /// <summary>
        /// Close dialog with parameter.
        /// </summary>
        /// <param name="dialog">Closable dialog.</param>
        /// <param name="parameter">Parameter.</param>
        public static void Close(this IClosableDialog dialog, object parameter)
        {
            dialog.OwnerIdentifier.Close(parameter);
        }

        /// <summary>
        /// Close dialog.
        /// </summary>
        /// <param name="dialog">Closable dialog.</param>
        public static void Close(this IClosableDialog dialog)
        {
            dialog.OwnerIdentifier.Close();
        }

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
        public static void Close(this IDialogIdentifier identifier)
        {
            identifier.Close(null);
        }

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
        public static async Task<object> Show(this IDialogIdentifier identifier, object content)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Show dialog with content.
        /// </summary>
        /// <param name="identifier">Dialog identifier.</param>
        /// <param name="content">Dialog content/</param>
        /// <returns></returns>
        [Obsolete("Use ShowAsync method", true)]
        public static async Task<T> Show<T>(this IDialogIdentifier identifier, object content)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
