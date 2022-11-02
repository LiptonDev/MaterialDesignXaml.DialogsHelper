using DevExpress.Mvvm;
using MaterialDesignXaml.DialogsHelper;
using System.Windows.Input;

namespace Example.Dialogs
{
    public class InputNameViewModel : IClosableDialog
    {
        public InputNameViewModel(IDialogIdentifier dialogIdentifier)
        {
            OwnerIdentifier = dialogIdentifier;
        }

        public ICommand CloseDialogCommand => new DelegateCommand<string>(name =>
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                this.Close(null);
            }
            else
            {
                this.Close(name);
            }
        });

        public IDialogIdentifier OwnerIdentifier { get; }
    }
}
