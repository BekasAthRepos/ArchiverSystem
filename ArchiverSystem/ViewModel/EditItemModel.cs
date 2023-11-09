using ArchiverSystem.Model;
using ArchiverSystem.Service;
using ArchiverSystem.View;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ArchiverSystem.ViewModel
{
    public class EditItemModel : INotifyPropertyChanged
    {
        private DAL _db;
        private Item _item;
        private BitmapImage _itemImage;
        private bool _defaultImage;
        private EditItemView _view;

        public Item Item
        {
            get { return _item; }
            set
            {
                if (_item != value)
                {
                    _item = value;
                    OnPropertyChanged(nameof(Item));
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

        public RelayCommand SaveItemCmd => new RelayCommand(execute => EditItem());
        public RelayCommand LoadImgCmd => new RelayCommand(execute => LoadImage());
        public event PropertyChangedEventHandler PropertyChanged;

        public EditItemModel(int itemId, EditItemView view)
        {
            Initialization(itemId);
            _view = view;   
        }

        private async void Initialization(int itemId)
        {
            _db = new DAL();
            ItemImage = new BitmapImage();
            Item = await _db.SelectItemByIdAsync(itemId);
            if (Item.Image == null)
            {
                _defaultImage = true;
                SetDefaultImage();
            }
            else
            {
                ItemImage = SetImage(_item.Image);
                /*
                using (MemoryStream stream = new MemoryStream(_item.Image))
                {
                    stream.Position = 0;
                    ItemImage.BeginInit();
                    ItemImage.CacheOption = BitmapCacheOption.OnLoad;
                    ItemImage.StreamSource = stream;
                    ItemImage.EndInit();
                }*/
            }
        }

        private async void EditItem()
        {
            int albumId = _item.AlbumId;
            if (String.IsNullOrEmpty(_item.Name))
            {
                MessageBox.Show(Application.Current.FindResource("nullField").ToString() + " " +
                    Application.Current.FindResource("name").ToString(),
                    Application.Current.FindResource("warning").ToString()
                    );
                return;
            }

            _item.UpdateDate = DateTime.Now;
            if (!_defaultImage)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(ItemImage));
                    encoder.Save(stream);
                    _item.Image = stream.ToArray();
                }
            }
            if (await _db.UpdateItemAsync(_item))
            {
                Messenger.Default.Send(new PropertyUpdateMessage
                {
                    PropertyName = "NotifyItemList",
                    Value = albumId
                });
                MessageBox.Show(Application.Current.FindResource("saveItem").ToString(),
                    Application.Current.FindResource("success").ToString()
                    );
                _view.Close();
            }
        }

        private void LoadImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files|*.jpg;*.bmp;*png";
            if (openFileDialog.ShowDialog() == true)
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

        public BitmapImage SetImage(byte[] array)
        {
            using (var ms = new MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
