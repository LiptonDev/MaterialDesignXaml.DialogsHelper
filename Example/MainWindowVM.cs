using DevExpress.Mvvm;
using MaterialDesignXaml.DialogsHelper;
using System.Windows;
using System.Windows.Input;

namespace Example
{
    class MainWindowVM : IDialogIdentifier
    {
        public string Identifier => "RootDialog";

        /// <summary>
        /// DelegateCommand - DevExpressMVVM.
        /// </summary>
        public ICommand OpenDialogCommand => new DelegateCommand(async () =>
        {
            //...any works

            string name = await this.Show<string>(new TestDialog(this));

            MessageBox.Show($"Your name: {name}");
        });
    }
}
