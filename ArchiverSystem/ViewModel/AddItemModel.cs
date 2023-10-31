using ArchiverSystem.Model;
using ArchiverSystem.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ArchiverSystem.ViewModel
{
    public class AddItemModel
    {
        private DAL db;
        private Item _newItem;

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
        public RelayCommand SaveItemCmd => new RelayCommand(execute => AddNewItem());
        public event PropertyChangedEventHandler PropertyChanged;

        public AddItemModel()
        {
            _newItem = new Item();
            db = new DAL();
        }

        private async void AddNewItem()
        {
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
            _newItem.Qty = 0;
            if (await db.InsertItemAsync(_newItem))
            {

                MessageBox.Show(Application.Current.FindResource("saveItem").ToString(),
                    Application.Current.FindResource("success").ToString()
                    );
                _newItem = new Item();
                OnPropertyChanged(nameof(NewItem));
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}