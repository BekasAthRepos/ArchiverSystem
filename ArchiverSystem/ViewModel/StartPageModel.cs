using ArchiverSystem.Model;
using ArchiverSystem.Service;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

namespace ArchiverSystem.ViewModel
{
    public class StartPageModel : INotifyPropertyChanged
    {
        private ObservableCollection<Album> _albumList;
        private ObservableCollection<Item> _itemList;
        private DAL _db;


        public ObservableCollection<Album> AlbumList
        {
            get { return _albumList; }
            set 
            { 
                _albumList = value;
                OnPropertyChanged(nameof(AlbumList));
            }
        }

        public ObservableCollection<Item> ItemList
        {
            get { return _itemList; }
            set
            {
                _itemList = value;
                OnPropertyChanged(nameof(ItemList));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand OnAlbumClickCmd => new RelayCommand(id => AlbumClick(id));
        public RelayCommand OnItemClickCmd => new RelayCommand(id => ItemClick(id));

        public StartPageModel()
        {
            Initialization();
        }

        private void Initialization()
        {
            _db = new DAL();
            FillAlbumList();
            Messenger.Default.Register<PropertyUpdateMessage>(this, message =>
            {
                if (message.PropertyName == "NotifyAlbumList")
                {
                    FillAlbumList();
                }
                if (message.PropertyName == "NotifyItemList")
                {
                    FillItemList((int)message.Value);
                }
                if (message.PropertyName == "DeleteAlbum")
                {
                    FillAlbumList();
                    ItemList.Clear();
                }
            });
        }

        private async void FillAlbumList()
        {
            List<Album> albums = await _db.SelectAlbumsAsync();
            AlbumList = new ObservableCollection<Album>(albums);
        }

        private async void FillItemList(int albumId)
        {
            List<Item> items = await _db.SelectAlbumItemsAsync(albumId);
            ItemList = new ObservableCollection<Item>(items);
        }

        private void AlbumClick(object id)
        {
            FillItemList((int)id);
        }

        private void ItemClick(object id)
        {
            
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
