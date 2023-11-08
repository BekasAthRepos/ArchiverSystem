using ArchiverSystem.Service;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ArchiverSystem.ViewModel
{
    public class DeleteItemModel
    {
        private DAL _db;
        private int _itemId, _albumId;

        public DeleteItemModel(int itemId, int albumId)
        {
            _itemId = itemId;
            _albumId = albumId;
            _db = new DAL();
        }

        public RelayCommand DeleteItemCmd => new RelayCommand(execute => DeleteItem());

        private async void DeleteItem()
        {
            if (await _db.DeleteItemByIdAsync(_itemId))
            {
                Messenger.Default.Send(new PropertyUpdateMessage
                {
                    PropertyName = "NotifyItemList",
                    Value = _albumId
                });

                MessageBox.Show(Application.Current.FindResource("itemDeleted").ToString(),
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
