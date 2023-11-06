using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiverSystem.Service
{
    public class PropertyUpdateMessage
    {
        public string PropertyName { get; set; }
        public object Value { get; set; }
    }
}
