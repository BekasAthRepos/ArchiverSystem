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
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Window
    {
        public StartPage()
        {
            InitializeComponent();
            this.Height = SystemParameters.PrimaryScreenHeight * 0.7;
            this.Width = SystemParameters.PrimaryScreenWidth * 0.7;
        }

        public void onAddAlbumClicked(object sender, EventArgs e)
        {
            AddAlbumView addAlbumView = new AddAlbumView();
            addAlbumView.ShowDialog();
        }
    }
}
