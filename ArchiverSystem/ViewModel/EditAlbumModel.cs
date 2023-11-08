using ArchiverSystem.Model;
using ArchiverSystem.Service;
using ArchiverSystem.View;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ArchiverSystem.ViewModel
{
    public class EditAlbumModel : INotifyPropertyChanged
    {
        private DAL _db;
        private EditAlbumView _view;
        private int _albumId;
        private Album _album;
        public Album Album
        {
            get { return _album; }
            set
            {
                if (_album != value)
                {
                    _album = value;
                    OnPropertyChanged(nameof(Album));
                }
            }
        }
        public RelayCommand SaveAlbumCmd => new RelayCommand(execute => SaveAlbum());
        public event PropertyChangedEventHandler PropertyChanged;

        public EditAlbumModel(int albumId, EditAlbumView view)
        {
            _albumId = albumId;
            _view = view;
            Initalization();
        }

        private async void Initalization()
        {
            _db = new DAL();
            Album = await _db.SelectAlbumByIdAsync(_albumId);
        }

        private async void SaveAlbum()
        {
            _album.Name = _album.Name.Trim();
            _album.Description = _album.Description.Trim();
            if (String.IsNullOrEmpty(_album.Name))
            {
                MessageBox.Show(Application.Current.FindResource("nullField").ToString() + " " +
                    Application.Current.FindResource("name").ToString(),
                    Application.Current.FindResource("warning").ToString()
                    );
                return;
            }

            _album.UpdateDate = DateTime.Now;
            if (await _db.UpdateAlbumAsync(_album))
            {
                Messenger.Default.Send(new PropertyUpdateMessage
                {
                    PropertyName = "NotifyAlbumList"
                });

                MessageBox.Show(Application.Current.FindResource("saveAlbum").ToString(),
                    Application.Current.FindResource("success").ToString()
                    );
                _view.Close();
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
