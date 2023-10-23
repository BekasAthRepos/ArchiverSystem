using ArchiverSystem.Model;
using ArchiverSystem.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.MobileControls;
using System.Windows;
using System.Windows.Input;

namespace ArchiverSystem.ViewModel
{
    public class AddAlbumModel
    {
        public Album NewAlbum { get; set; }
        public RelayCommand SaveAlbumCmd => new RelayCommand(execute => AddNewAlbum());

        private DAL db;

        public AddAlbumModel() { }

        private async void AddNewAlbum()
        {
            if (String.IsNullOrEmpty(NewAlbum.Name))
            {
                MessageBox.Show(Application.Current.FindResource("nullField").ToString() + " " +
                    Application.Current.FindResource("name").ToString(),
                    Application.Current.FindResource("warning").ToString()
                    );
                return;
            }

            await db.InsertAlbumAsync();
        }
    }
}
