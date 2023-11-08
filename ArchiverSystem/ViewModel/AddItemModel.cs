using ArchiverSystem.Model;
using ArchiverSystem.Service;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ArchiverSystem.ViewModel
{
    public class AddItemModel: INotifyPropertyChanged
    {
        private DAL _db;
        private Item _newItem;
        private BitmapImage _itemImage;
        private bool _defaultImage;

        public Item NewItem
        {
            get { return _newItem; }
            set
            {
                if (_newItem != value)
                {
                    _newItem = value;
                    OnPropertyChanged(nameof(NewItem));
                }
            }
        }

        public BitmapImage ItemImage
        {
            get { return _itemImage; }
            set
            {
                if (_itemImage != value)
                {
                    _itemImage = value;
                    OnPropertyChanged(nameof(ItemImage));
                }
            }
        }

        public RelayCommand SaveItemCmd => new RelayCommand(execute => AddNewItem());
        public RelayCommand LoadImgCmd => new RelayCommand(execute => LoadImage());
        public event PropertyChangedEventHandler PropertyChanged;

        public AddItemModel(int albumId)
        {
            Initialization(albumId);
        }

        private void Initialization(int albumId)
        {
            _db = new DAL();
            _newItem = new Item();
            _newItem.AlbumId = albumId;
            _newItem.Qty = 1;
            SetDefaultImage();
        }

        private async void AddNewItem()
        {
            int albumId = _newItem.AlbumId;
            if (String.IsNullOrEmpty(_newItem.Name))
            {
                MessageBox.Show(Application.Current.FindResource("nullField").ToString() + " " +
                    Application.Current.FindResource("name").ToString(),
                    Application.Current.FindResource("warning").ToString()
                    );
                return;
            }

            _newItem.InputDate = DateTime.Now;
            _newItem.UpdateDate = DateTime.Now;
            if (!_defaultImage)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder(); // Or appropriate encoder
                    encoder.Frames.Add(BitmapFrame.Create(ItemImage));
                    encoder.Save(stream);
                    _newItem.Image = stream.ToArray();
                }
            }
            if (await _db.InsertItemAsync(_newItem))
            {
                Messenger.Default.Send(new PropertyUpdateMessage
                {
                    PropertyName = "NotifyItemList",
                    Value = albumId
                });
                MessageBox.Show(Application.Current.FindResource("saveItem").ToString(),
                    Application.Current.FindResource("success").ToString()
                    );
                NewItem = new Item();
                SetDefaultImage();
            }
        }

        private void LoadImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files|*.jpg;*.bmp;*png";
            if(openFileDialog.ShowDialog() == true)
            {
                ItemImage = new BitmapImage(new Uri(openFileDialog.FileName));
                _defaultImage = false;
            }
        }

        private void SetDefaultImage()
        {
            ItemImage = new BitmapImage(new Uri(Application.Current.FindResource("newItemImg").ToString()));
            _defaultImage = true;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}