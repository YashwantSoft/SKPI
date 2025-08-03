using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPApplication
{
    public class ProductClassNew
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int CategoryID { get; set; }
    }

    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

}
