using MaterialDesignXaml.DialogsHelper.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace MaterialDesignXaml.DialogsHelper.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для MaterialMessageBox.xaml
    /// </summary>
    public partial class MaterialMessageBox : UserControl
    {
        public MaterialMessageBox(object content, params MaterialMessageBoxButton[] buttons) : this(content)
        {
            InitializeComponent();

            Buttons = new ObservableCollection<MaterialMessageBoxButton>(buttons);
        }

        public MaterialMessageBox(object content, IEnumerable<MaterialMessageBoxButton> buttons) : this(content)
        {
            InitializeComponent();

            Buttons = new ObservableCollection<MaterialMessageBoxButton>(buttons);
        }

        private MaterialMessageBox(object content)
        {
            InitializeComponent();

            DataContext = this;
            DialogContent = content;
        }

        /// <summary>
        /// Current content.
        /// </summary>
        public object DialogContent { get; set; }

        /// <summary>
        /// Buttons.
        /// </summary>
        public ObservableCollection<MaterialMessageBoxButton> Buttons { get; }
    }
}
