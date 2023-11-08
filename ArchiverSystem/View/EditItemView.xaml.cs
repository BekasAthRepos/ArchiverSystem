using ArchiverSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
