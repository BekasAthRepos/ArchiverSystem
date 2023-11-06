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
    public partial class AddItemView : Window
    {
        private AddItemModel _addItemModel;
        private int _albumId;

        public AddItemView(int albumId)
        {
            InitializeComponent();
            _addItemModel = new AddItemModel(albumId);
            DataContext = _addItemModel;
            _albumId = albumId;
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
