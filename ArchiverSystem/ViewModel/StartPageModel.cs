using ArchiverSystem.Model;
using ArchiverSystem.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ArchiverSystem.ViewModel
{
    public class StartPageModel : INotifyPropertyChanged
    {
        private ObservableCollection<Album> _albumList;
        private DAL db;


        public List<Album> AlbumList
        {
            get { return _albumList; }
            set 
            { 
                _albumList = value;
                OnPropertyChanged(nameof(AlbumList));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        
        public StartPageModel()
        {
            db = new DAL();
            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            _albumList = await db.SelectAlbumsAsync();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
