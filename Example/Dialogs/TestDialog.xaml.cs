using MaterialDesignXaml.DialogsHelper;
using System.Windows.Controls;

namespace Example.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для TestDialog.xaml
    /// </summary>
    public partial class TestDialog : UserControl
    {
        public TestDialog(IDialogIdentifier identifier)
        {
            InitializeComponent();

            DataContext = new TestDialogViewModel(identifier);
        }
    }
}
