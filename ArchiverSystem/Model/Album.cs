
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ArchiverSystem.Model
{
    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Item> Items { get; set; }
        public DateTime InputDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public Album() { }

        public void UpdateAlbum()
        {
            UpdateDate = DateTime.Now;
        }
    }
}