using MaterialDesignXaml.DialogsHelper.Enums;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace MaterialDesignXaml.DialogsHelper.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для MaterialMessageBox.xaml
    /// </summary>
    public partial class MaterialMessageBox : UserControl
    {
        public MaterialMessageBox(object content, MaterialMessageBoxButtons buttons)
        {
            InitializeComponent();

            DataContext = this;

            if (buttons.HasFlag(MaterialMessageBoxButtons.No))
            {
                Buttons.Add(MaterialMessageBoxButtons.No);
            }
            if (buttons.HasFlag(MaterialMessageBoxButtons.Yes))
            {
                Buttons.Add(MaterialMessageBoxButtons.Yes);
            }
            if (buttons.HasFlag(MaterialMessageBoxButtons.Cancel))
            {
                Buttons.Add(MaterialMessageBoxButtons.Cancel);
            }
            if (buttons.HasFlag(MaterialMessageBoxButtons.Ok))
            {
                Buttons.Add(MaterialMessageBoxButtons.Ok);
            }

            DialogContent = content;
        }

        /// <summary>
        /// Current content.
        /// </summary>
        public object DialogContent { get; set; }

        /// <summary>
        /// Buttons.
        /// </summary>
        public ObservableCollection<MaterialMessageBoxButtons> Buttons { get; } = new ObservableCollection<MaterialMessageBoxButtons>();
    }
}
