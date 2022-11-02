using DevExpress.Mvvm;
using MaterialDesignXaml.DialogsHelper;
using MaterialDesignXaml.DialogsHelper.Enums;
using MaterialDesignXaml.DialogsHelper.Models;
using System.Collections.Generic;
using System.Windows.Input;

namespace MultiExample
{
    public class ViewModel : IMultiDialogIdentifier
    {
        public List<IDialogIdentifier> DialogIdentifiers { get; }

        public ViewModel()
        {
            DialogIdentifiers = new List<IDialogIdentifier>
            {
                new DialogIdentifier("FirstIdentifier"),
                new DialogIdentifier("SecondIdentifier")
            };
        }

        public ICommand OpenFirstDialogCommand => new DelegateCommand(() => ShowMessageBox(DialogIdentifiers[0]));

        public ICommand OpenSecondDialogCommand => new DelegateCommand(() => ShowMessageBox(DialogIdentifiers[1]));

        public ICommand CloseAllDialogsCommand => new DelegateCommand(() =>
        {
            this.CloseAll(null); //see line #47
        });

        async void ShowMessageBox(IDialogIdentifier identifier)
        {
            var buttons = new List<MaterialMessageBoxButton>
            {
                new MaterialMessageBoxButton("Ok", MaterialMessageBoxButtonType.Ok),
                new MaterialMessageBoxButton("Cancel", MaterialMessageBoxButtonType.Cancel),
                new MaterialMessageBoxButton("Nothing", null)
            };

            var res = await identifier.ShowMessageBoxAsync("Press any button", buttons);

            if (res == null)
            {
                await identifier.ShowMessageBoxAsync($"ABORTED!", buttons[0]);
            }
            else
            {
                await identifier.ShowMessageBoxAsync($"You pressed: {res}", buttons[0]);
            }
        }
    }
}
