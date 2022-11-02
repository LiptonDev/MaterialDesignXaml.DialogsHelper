using DevExpress.Mvvm;
using Example.Dialogs;
using MaterialDesignXaml.DialogsHelper;
using MaterialDesignXaml.DialogsHelper.Enums;
using MaterialDesignXaml.DialogsHelper.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Example
{
    public class ViewModel : IDialogIdentifier
    {
        public string Identifier => "RootDialog";

        public ICommand InputNameCommand => new DelegateCommand(async () =>
        {
            var name = await this.ShowAsync<string>(new InputNameDialog(this));

            await this.ShowMessageBoxAsync($"Your name is = {name}", new MaterialDesignXaml.DialogsHelper.Models.MaterialMessageBoxButton("Ok", null));
        });

        public ICommand FiveSecondsCommand => new DelegateCommand(async () => 
        {
            var block = new TextBlock() { Text = "5 sec.", Margin = new System.Windows.Thickness(5), FontSize = 25 };
            await this.ShowAsync(block, open);
            //await this.ShowAsync("5 sec.", open);

            async void open()
            {
                for (int i = 4; i >= 0; i--)
                {
                    await Task.Delay(1000);
                    //this.UpdateContent($"{i} sec.");
                    block.Text = $"{i} sec.";
                }

                this.Close();
            }
        });

        public ICommand MessageBoxCommand => new DelegateCommand(async () =>
        {
            var block = new TextBlock() { Margin = new System.Windows.Thickness(5), FontSize = 25 };
            block.Inlines.Add(new Run { Text = "Hello, " });
            block.Inlines.Add(new Run { Text = "World", Foreground = Brushes.Red });

            var buttons = new List<MaterialMessageBoxButton>
            {
                new MaterialMessageBoxButton("Hello", "Hello"),
                new MaterialMessageBoxButton("World", "World"),
                new MaterialMessageBoxButton("Cancel", MaterialMessageBoxButtonType.Cancel)
            };

            var res = await this.ShowMessageBoxAsync(block, buttons);

            await this.ShowMessageBoxAsync(res, new MaterialMessageBoxButton("Ok", null));
        });
    }
}
