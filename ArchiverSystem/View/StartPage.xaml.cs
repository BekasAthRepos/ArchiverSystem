using ArchiverSystem.Model;
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
    public partial class StartPage : Window
    {
        StartPageModel _startPageModel;
        private int _selectedAlbumId;
        public StartPage()
        {
            InitializeComponent();
            _startPageModel = new StartPageModel();
            DataContext = _startPageModel;

            this.Height = SystemParameters.PrimaryScreenHeight * 0.7;
            this.Width = SystemParameters.PrimaryScreenWidth * 0.7;
        }

        private void onAddAlbumClicked(object sender, EventArgs e)
        {
            AddAlbumView addAlbumView = new AddAlbumView();
            addAlbumView.ShowDialog();
        }

        private void onAlbumClicked(object sender, MouseButtonEventArgs e)
        {
            var lAlbum = (sender as ListView).SelectedItem;
            if(lAlbum != null)
            {
                Album album = lAlbum as Album;
                _selectedAlbumId = album.Id;
                _startPageModel.OnAlbumClick.Execute(album.Id);
            }
        }

        private void onAddItemClicked(object sender, EventArgs e)
        {
            if(_selectedAlbumId > 0)
            {
                AddItemView addItemView = new AddItemView(_selectedAlbumId);
                addItemView.ShowDialog();
            }else
                MessageBox.Show(Application.Current.FindResource("noAlbumSelected").ToString());
        }

        private void onItemClicked(object sender, MouseButtonEventArgs e)
        {
            //var lAlbum = (sender as ListView).SelectedItem;
            //Album album = lAlbum as Album;
            //_startPageModel.OnAlbumClick.Execute(album.Id);
        }
    }
}
