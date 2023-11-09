using ArchiverSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ArchiverSystem.View
{

    public partial class EditItemView : Window
    {
        public EditItemView(int itemId)
        {
            EditItemModel editItemModel = new EditItemModel(itemId, this);
            DataContext = editItemModel;
            InitializeComponent();
            this.Height = SystemParameters.PrimaryScreenHeight * 0.5;
            this.Width = SystemParameters.PrimaryScreenWidth * 0.25;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]+");
            string proposedText = (sender as TextBox).Text + e.Text;
            if (proposedText.Count(c => c == '.') > 1)
            {
                e.Handled = true;
                return;
            }
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
