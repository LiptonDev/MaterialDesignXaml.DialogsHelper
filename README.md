# MaterialDesignXaml.DialogsHelper

![Example](anim.gif)

## MVVM Example:

Main ViewModel:
```C#
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
```

MainWindow View:
```XAML
<Window x:Class="Example.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:Example"
        xmlns:materialDesign="http://materialdesigninxaml.net/winfx/xaml/themes"
        xmlns:controls="clr-namespace:MaterialDesignXaml.DialogsHelper.Controls;assembly=MaterialDesignXaml.DialogsHelper"
        mc:Ignorable="d"
        Title="MainWindow" 
        Height="450" 
        Width="800">

    <Window.DataContext>
        <local:MainWindowVM/>
    </Window.DataContext>

    <Window.Resources>
        <!-- Style for MESSAGEBOX buttons (!) -->
        <Style TargetType="controls:DialogButton"> <!-- !!! -->
            <Setter Property="Margin" Value="2"/>
            <Setter Property="FontSize" Value="15"/>
        </Style>

        <Style TargetType="Button">
            <Setter Property="Height" Value="50"/>
            <Setter Property="Width" Value="150"/>
            <Setter Property="Margin" Value="5"/>
        </Style>
    </Window.Resources>

    <materialDesign:DialogHost Identifier="{Binding Identifier}">
        <StackPanel>
            <Button Content="Open dialog"
                    Command="{Binding OpenDialogCommand}"/>

            <Button Content="Open 2 sec dialog"
                    Command="{Binding Open2SecDialogCommand}"/>

            <Button Content="Open messagebox"
                    Command="{Binding OpenMessageBox}"/>
        </StackPanel>
    </materialDesign:DialogHost>

</Window>
```

TestDialog (UserControl):
```C#
public partial class TestDialog : UserControl
{
	public TestDialog(IDialogIdentifier identifier)
	{
		InitializeComponent();

		DataContext = new TestDialogVM(identifier);
	}
}
```

TestDialog ViewModel:
```C#
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
		this.Close(name); //closing with parameter = value
	});

	/// <summary>
	/// DialogHost owner.
	/// </summary>
	public IDialogIdentifier Identifier { get; set; }
}
```

TestDialog View:
```XAML
<UserControl ...>

    <StackPanel>
        <TextBlock Text="Input your name"/>

        <TextBox Name="Name"/>

        <Button Content="Close dialog"
                Command="{Binding CloseDialogCommand}"
                CommandParameter="{Binding Text, ElementName=Name}"/>
    </StackPanel>

</UserControl>
```
