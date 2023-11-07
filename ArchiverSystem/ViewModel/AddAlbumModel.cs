using ArchiverSystem.Model;
using ArchiverSystem.Service;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.MobileControls;
using System.Windows;
using System.Windows.Input;

namespace ArchiverSystem.ViewModel
{
    public class AddAlbumModel : INotifyPropertyChanged
    {
        private DAL _db;
        private Album _newAlbum;

        public Album NewAlbum
        {
            get { return _newAlbum; }
            set
            {
                if (_newAlbum != value)
                {
                    _newAlbum = value;
                    OnPropertyChanged(nameof(NewAlbum));
                }
            }
        }
        public RelayCommand SaveAlbumCmd => new RelayCommand(execute => AddNewAlbum());
        public event PropertyChangedEventHandler PropertyChanged;


        public AddAlbumModel() 
        {
            _newAlbum = new Album();
            _db = new DAL();
        }

        private async void AddNewAlbum()
        {
            if (String.IsNullOrEmpty(_newAlbum.Name))
            {
                MessageBox.Show(Application.Current.FindResource("nullField").ToString() + " " +
                    Application.Current.FindResource("name").ToString(),
                    Application.Current.FindResource("warning").ToString()
                    );
                return;
            }

            _newAlbum.InputDate = DateTime.Now;
            _newAlbum.UpdateDate = DateTime.Now;
            if(await _db.InsertAlbumAsync(_newAlbum))
            {

                MessageBox.Show(Application.Current.FindResource("saveAlbum").ToString(), 
                    Application.Current.FindResource("success").ToString()
                    );
                NewAlbum = new Album();

                Messenger.Default.Send(new PropertyUpdateMessage
                {
                    PropertyName = "AlbumList"
                });
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
