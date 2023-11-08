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
        public StartPage()
        {
            InitializeComponent();
            _startPageModel = new StartPageModel();
            DataContext = _startPageModel;

            this.Height = SystemParameters.PrimaryScreenHeight * 0.7;
            this.Width = SystemParameters.PrimaryScreenWidth * 0.7;
        }

        private void onAlbumClicked(object sender, EventArgs e)
        {
            if(AlbumList.SelectedIndex >= 0)
            {
                Album album = (Album)AlbumList.SelectedItem;
                if (album.Id > 0)
                {
                    _startPageModel.OnAlbumClickCmd.Execute(album.Id);
                }
            }
        }

        private void onAddAlbumClicked(object sender, EventArgs e)
        {
            AddAlbumView addAlbumView = new AddAlbumView();
            addAlbumView.ShowDialog();
        }

        private void onEditAlbumClicked(object sender, EventArgs e)
        {
            if (AlbumList.SelectedIndex >= 0)
            {
                Album album = (Album)AlbumList.SelectedItem;
                if (album.Id > 0)
                {
                    EditAlbumView editAlbum = new EditAlbumView(album.Id);
                    editAlbum.ShowDialog();
                }
            }
            else
                MessageBox.Show(Application.Current.FindResource("noAlbumSelected").ToString());
        }

        private void onDeleteAlbumClicked(object sender, EventArgs e)
        {
            if (AlbumList.SelectedIndex >= 0)
            {
                Album album = (Album)AlbumList.SelectedItem;
                if (album.Id > 0)
                {
                    var Result = MessageBox.Show(Application.Current.FindResource("deletedAlbum?").ToString(),
                    Application.Current.FindResource("warning").ToString(),
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (Result == MessageBoxResult.Yes)
                    {
                        DeleteAlbumModel deleteAlbum = new DeleteAlbumModel();
                        deleteAlbum.DeleteAlbumCmd.Execute(album.Id);
                    }
                }
            }
            else
                MessageBox.Show(Application.Current.FindResource("noAlbumSelected").ToString());
        }

        private void onAddItemClicked(object sender, EventArgs e)
        {
            if (AlbumList.SelectedIndex >= 0)
            {
                Album album = (Album)AlbumList.SelectedItem;
                if (album.Id > 0)
                {
                    AddItemView addItemView = new AddItemView(album.Id);
                    addItemView.ShowDialog();
                }
            }
            else
                MessageBox.Show(Application.Current.FindResource("noAlbumSelected").ToString());
        }

        private void onEditItemClicked(object sender, EventArgs e)
        {
            if (ItemList.SelectedIndex >= 0)
            {
                Item item = (Item)ItemList.SelectedItem;
                if (item.Id > 0)
                {
                    EditItemView editAlbum = new EditItemView(item.Id);
                    editAlbum.ShowDialog();
                }
            }
            else
                MessageBox.Show(Application.Current.FindResource("noItemSelected").ToString());
        }

        private void onDeleteItemClicked(object sender, EventArgs e)
        {
            if(ItemList.SelectedIndex >= 0)
            {
                Item item = (Item) ItemList.SelectedItem;
                if ( item.Id > 0)
                {
                    var Result = MessageBox.Show(Application.Current.FindResource("deletedItem?").ToString(),
                    Application.Current.FindResource("warning").ToString(),
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (Result == MessageBoxResult.Yes)
                    {
                        DeleteItemModel deleteItem = new DeleteItemModel(item.Id, item.AlbumId);
                        deleteItem.DeleteItemCmd.Execute(null);
                    }
                }
            }
            else
                MessageBox.Show(Application.Current.FindResource("noItemSelected").ToString());
        }

        private void onItemClicked(object sender, EventArgs e)
        {
            if (ItemList.SelectedIndex >= 0)
            {
                Item item = (Item)ItemList.SelectedItem;
                if (item.Id > 0)
                {
                    _startPageModel.OnItemClickCmd.Execute(item.Id);
                }
            }
        }
    }
}
