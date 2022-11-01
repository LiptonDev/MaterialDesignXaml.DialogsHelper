using DevExpress.Mvvm;
using Example.Dialogs;
using MaterialDesignXaml.DialogsHelper;
using MaterialDesignXaml.DialogsHelper.Enums;
using MaterialDesignXaml.DialogsHelper.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Example
{
    public class MainViewModel : IDialogIdentifier
    {
        public string Identifier => "RootDialog";

        public ICommand OpenDialogCommand => new DelegateCommand(async () =>
        {
            //...any work

            //this - IDialogIdentifier
            string? name = await this.ShowAsync<string?>(new TestDialog(this), OpenHandler, CloseHandler);

            if (name is not null)
            {
                MessageBox.Show($"Your name: {name}");
            }
            else
            {
                MessageBox.Show("Name is null");
            }
        });

        public ICommand Open5SecDialogCommand => new DelegateCommand(async () =>
        {
            await this.ShowAsync("5 seconds", Close);

            async void Close()
            {
                for (int i = 4; i >= 0; i--)
                {
                    await Task.Delay(1000);
                    this.UpdateContent($"{i} seconds"); //this - IDialogIdentifier
                }

                this.Close(); //this - IDialogIdentifier
            }
        });

        public ICommand OpenMessageBox => new DelegateCommand(async () =>
        {
            //...any work

            var buttons = new List<MaterialMessageBoxButton>
            {
                new MaterialMessageBoxButton("Ok / Ок", MaterialMessageBoxButtonType.Ok),
                new MaterialMessageBoxButton("Yes / Да", MaterialMessageBoxButtonType.Yes),
                new MaterialMessageBoxButton("No / Нет", MaterialMessageBoxButtonType.No),
                new MaterialMessageBoxButton("Cancel / Отмена", MaterialMessageBoxButtonType.Cancel),
                new MaterialMessageBoxButton("Current DateTime", DateTime.Now),
                new MaterialMessageBoxButton("Example string", "Hello, Git!")
            };

            //this - IDialogIdentifier
            var result = await this.ShowMessageBoxAsync("What you'll press?", buttons, OpenHandler, CloseHandler);

            await this.ShowMessageBoxAsync($"You pressed: {result}", buttons[0], buttons[1]);
        });

        private void OpenHandler()
        {
            Console.WriteLine("Open");
        }

        private void CloseHandler()
        {
            Console.WriteLine("Close");
        }
    }
}
