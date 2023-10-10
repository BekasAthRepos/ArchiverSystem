using ArchiverSystem.Model;
using ArchiverSystem.Service;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ArchiverSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DAL dal;

        public MainWindow()
        {
            InitializeComponent();
            dal = new DAL();
        }

        public async void insertalbum(object sender, RoutedEventArgs e)
        { 
            if(await dal.DeleteAlbumByIdAsync(15))
                lbl1.Content = "deleted";
        }
    }
}
