using MaterialDesignXaml.DialogsHelper;
using System.Windows.Controls;

namespace Example.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для InputNameDialog.xaml
    /// </summary>
    public partial class InputNameDialog : UserControl
    {
        public InputNameDialog(IDialogIdentifier identifier)
        {
            InitializeComponent();

            DataContext = new InputNameViewModel(identifier);
        }
    }
}
