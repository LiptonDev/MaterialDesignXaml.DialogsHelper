using MaterialDesignXaml.DialogsHelper;
using System.Windows.Controls;

namespace Example
{
    /// <summary>
    /// Логика взаимодействия для TestDialog.xaml
    /// </summary>
    public partial class TestDialog : UserControl
    {
        public TestDialog(IDialogIdentifier identifier)
        {
            InitializeComponent();

            DataContext = new TestDialogVM(identifier);
        }
    }
}
