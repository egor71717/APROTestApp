using APROTestApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APROTestApp.DAL.EF
{
    public class ApplicationDBContext: DbContext
    {
        private static ApplicationDBContext instance = null;

        public DbSet<Producer> Producers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        static ApplicationDBContext()
        {
            Database.SetInitializer(new DbInitializer());
        }   

        private ApplicationDBContext(String connectionStringName = "DefaultConnection") : base(connectionStringName){}

        public static ApplicationDBContext GetInstance(String connectionStringName)
        {
            if (instance == null)
                instance = new ApplicationDBContext(connectionStringName);
            return instance;
        }
    }
}
