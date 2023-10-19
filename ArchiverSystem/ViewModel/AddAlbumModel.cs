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
        public string Name { get; set; }
        public string Description { get; set; }
        public RelayCommand SaveAlbumCmd => new RelayCommand(execute => AddNewAlbum());
        private ResourceDictionary _resourceDictionary;

        public AddAlbumModel() 
        {
            _resourceDictionary = new ResourceDictionary();
           // _resourceDictionary.Source = new Uri()
        }

        private void AddNewAlbum()
        {
            if (String.IsNullOrEmpty(Name))
            {
                MessageBox.Show(Name);
            }
        }

        private bool CheckFilledField(String field)
        {
            if (String.IsNullOrEmpty(field))
            {
                //MessageBox.Show();
                return false;
            }
            return false;
        }
    }
}
