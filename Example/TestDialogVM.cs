using DevExpress.Mvvm;
using MaterialDesignXaml.DialogsHelper;
using System.Windows.Input;

namespace Example
{
    class TestDialogVM : IClosableDialog
    {
        public TestDialogVM(IDialogIdentifier identifier)
        {
            Identifier = identifier;
        }

        /// <summary>
        /// Closing dialog.
        /// DelegateCommand - DevExpressMVVM.
        /// </summary>
        public ICommand CloseDialogCommand => new DelegateCommand<string>(name =>
        {
            //...any works

            //closure options

            //1.
            //this.Close(); //closing with parameter = null

            //2.
            this.Close(name); //closing with parameter = value

            //3.
            //Identifier.Close(); //closing with parameter = null

            //4.
            //Identifier.Close(value); //closing with parameter = value
        });

        /// <summary>
        /// DialogHost owner.
        /// </summary>
        public IDialogIdentifier Identifier { get; set; }
    }
}
