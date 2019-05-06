using DevExpress.Mvvm;
using MaterialDesignXaml.DialogsHelper;
using MaterialDesignXaml.DialogsHelper.Enums;
using System.Threading.Tasks;
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
            //...any work

            string name = await this.ShowAsync<string>(new TestDialog(this), () => System.Console.WriteLine("Hi"), () => System.Console.WriteLine("Bye"));

            MessageBox.Show($"Your name: {name}");
        });

        /// <summary>
        /// DelegateCommand - DevExpressMVVM.
        /// </summary>
        public ICommand Open2SecDialogCommand => new DelegateCommand(async () =>
        {
            await this.ShowAsync("This dialog closes within 2 seconds.", async () =>
            {
                await Task.Delay(2000);

                this.Close();
            });
        });

        /// <summary>
        /// DelegateCommand - DevExpressMVVM.
        /// </summary>
        public ICommand OpenMessageBox => new DelegateCommand(async () =>
        {
            //...any work

            var result = await this.ShowMessageBoxAsync("What you'll press?", MaterialMessageBoxButtons.All);

            await this.ShowMessageBoxAsync($"You pressed: {result}", MaterialMessageBoxButtons.Ok);
        });
    }
}
