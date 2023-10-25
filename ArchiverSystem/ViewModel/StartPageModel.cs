using ArchiverSystem.Model;
using ArchiverSystem.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ArchiverSystem.ViewModel
{
    public class StartPageModel : INotifyPropertyChanged
    {
        private ObservableCollection<Album> _albumList;
        private DAL db;


        public ObservableCollection<Album> AlbumList
        {
            get { return _albumList; }
            set 
            { 
                _albumList = value;
                OnPropertyChanged(nameof(AlbumList));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand OnAlbumClick => new RelayCommand(execute => AlbumClick());
        
        public StartPageModel()
        {
            db = new DAL();
            FillAlbumList();
        }

        private async void FillAlbumList()
        {
            List<Album> albums = await db.SelectAlbumsAsync();
            _albumList = new ObservableCollection<Album>(albums);
            OnPropertyChanged(nameof(AlbumList));
        }

        private async void AlbumClick()
        {
            MessageBox.Show("Album " + " Clicked");
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
