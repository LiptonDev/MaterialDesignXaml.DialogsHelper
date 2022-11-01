using DevExpress.Mvvm;
using MaterialDesignXaml.DialogsHelper;
using System.Windows.Input;

namespace Example.Dialogs
{
    public class TestDialogViewModel : IClosableDialog
    {
        public TestDialogViewModel(IDialogIdentifier ownerIdentifier)
        {
            OwnerIdentifier = ownerIdentifier;
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
