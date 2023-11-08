using ArchiverSystem.Service;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ArchiverSystem.ViewModel
{
    public class DeleteAlbumModel
    {
        private DAL _db;

        public DeleteAlbumModel()
        {
            _db = new DAL();
        }

        public RelayCommand DeleteAlbumCmd => new RelayCommand(id => DeleteAlbum(id));

        private async void DeleteAlbum(object albumId)
        {
            if(await _db.DeleteAlbumByIdAsync((int)albumId))
            {
                Messenger.Default.Send(new PropertyUpdateMessage
                {
                    PropertyName = "DeleteAlbum"
                });

                MessageBox.Show(Application.Current.FindResource("albumDeleted").ToString(),
                    Application.Current.FindResource("success").ToString()
                    );
            }
            else
            {
                MessageBox.Show("", Application.Current.FindResource("error").ToString());
            }
        }
    }
}
