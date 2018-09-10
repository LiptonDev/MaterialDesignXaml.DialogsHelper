using MaterialDesignThemes.Wpf;
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

        /// <summary>
        /// Show dialog with content.
        /// </summary>
        /// <param name="identifier">Dialog identifier.</param>
        /// <param name="content">Dialog content/</param>
        /// <returns></returns>
        public static async Task<object> Show(this IDialogIdentifier identifier, object content) =>
            await DialogHost.Show(content, identifier.Identifier, (_, e) => Sessions.Add(identifier.Identifier, e.Session), (_, e) => Sessions.Remove(identifier.Identifier));

        /// <summary>
        /// Show dialog with content.
        /// </summary>
        /// <param name="identifier">Dialog identifier.</param>
        /// <param name="content">Dialog content/</param>
        /// <returns></returns>
        public static async Task<T> Show<T>(this IDialogIdentifier identifier, object content) =>
            (T)await identifier.Show(content);

        /// <summary>
        /// Update dialog content.
        /// </summary>
        /// <param name="identifier">Dialog identifier.</param>
        /// <param name="content">New content.</param>
        public static void UpdateContent(this IDialogIdentifier identifier, object content) => Sessions[identifier.Identifier].UpdateContent(content);

        /// <summary>
        /// Close dialog with parameter.
        /// </summary>
        /// <param name="dialog">Closable dialog.</param>
        /// <param name="parameter">Parameter.</param>
        public static void Close(this IClosableDialog dialog, object parameter) => dialog.Identifier.Close(parameter);

        /// <summary>
        /// Close dialog.
        /// </summary>
        /// <param name="dialog">Closable dialog.</param>
        public static void Close(this IClosableDialog dialog) => dialog.Identifier.Close();

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
    }
}
