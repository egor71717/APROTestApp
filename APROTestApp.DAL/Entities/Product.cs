using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APROTestApp.DAL.Entities
{
    public class Product
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public Double Price { get; set; }
        public String OnlinerURL { get; set; }
        public Byte[] Picture { get; set; }
        public String Description { get; set; }
        public virtual Category Category { get; set; }
        public virtual Producer Pruducer { get; set; }
    }
}
