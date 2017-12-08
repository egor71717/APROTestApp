using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APROTestApp.DAL.Entities
{
    public class Category
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
