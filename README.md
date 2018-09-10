# MaterialDesignXaml.DialogsHelper

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
		//...any works

		string name = await this.Show<string>(new TestDialog(this));

		MessageBox.Show($"Your name: {name}");
	});
}
```

MainWindow View:
```XAML
<Window ...>

    <Window.DataContext>
        <local:MainWindowVM/>
    </Window.DataContext>

    <materialDesign:DialogHost Identifier="{Binding Identifier}">
	
        <Button Content="Open dialog"
                Command="{Binding OpenDialogCommand}"
                Height="50"
                Width="100"/>
				
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
