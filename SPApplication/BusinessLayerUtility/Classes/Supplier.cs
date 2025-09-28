using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayerUtility.Classes
{
    public class SupplierClass
    {
        public int ID { get; set; }
        public string SupplierName { get; set; }

        public override string ToString()
        {
            return SupplierName; // Display Name in ComboBox
        }
    }
}
