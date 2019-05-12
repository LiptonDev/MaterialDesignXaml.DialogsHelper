using DevExpress.Mvvm;
using MaterialDesignXaml.DialogsHelper;
using MaterialDesignXaml.DialogsHelper.Enums;
using System.Collections.Generic;
using System.Windows.Input;

namespace MultiExample
{
    class MainVM : IMultiDialogIdentifier
    {
        public List<IDialogIdentifier> DialogIdentifiers { get; }

        public MainVM()
        {
            DialogIdentifiers = new List<IDialogIdentifier>
            {
                //DialogIdentifier - implementation for interface.
                new DialogIdentifier("first"),
                new DialogIdentifier("second")
            };
        }

        /// <summary>
        /// DelegateCommand - DevExpressMVVM.
        /// </summary>
        public ICommand OpenFirstDialogCommand => new DelegateCommand(() => ShowMessageBox(DialogIdentifiers[0]));

        /// <summary>
        /// DelegateCommand - DevExpressMVVM.
        /// </summary>
        public ICommand OpenSecondDialogCommand => new DelegateCommand(() => ShowMessageBox(DialogIdentifiers[1]));

        /// <summary>
        /// DelegateCommand - DevExpressMVVM.
        /// </summary>
        public ICommand CloseAllDialogsCommand => new DelegateCommand(() =>
        {
            this.CloseAll(MaterialMessageBoxButtons.Nothing);
            //OR
            //DialogIdentifiers[0].Close(MaterialMessageBoxButtons.Ok);
            //DialogIdentifiers[1].Close(MaterialMessageBoxButtons.Yes);
            //OR
            //DialogIdentifiers[0].Close(MaterialMessageBoxButtons.Nothing);
            //DialogIdentifiers[1].Close(MaterialMessageBoxButtons.Nothing);
        });

        async void ShowMessageBox(IDialogIdentifier identifier)
        {
            var res = await identifier.ShowMessageBoxAsync("Press any button", MaterialMessageBoxButtons.All);

            if (res == MaterialMessageBoxButtons.Nothing) //returns "Nothing" when dialog closing (look in CloseAllDialogsCommand).
                await identifier.ShowMessageBoxAsync("Aborted!", MaterialMessageBoxButtons.Ok);
            else await identifier.ShowMessageBoxAsync($"You pressed: {res}", MaterialMessageBoxButtons.Ok);
        }
    }
}
