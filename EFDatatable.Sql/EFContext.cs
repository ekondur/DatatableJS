using EFDatatable.Sql.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDatatable.Sql
{
    public class EFContext : DbContext
    {
        public EFContext()
            : base("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EKDb;Integrated Security=true;MultipleActiveResultSets=true")
        {

        }

        public DbSet<Customer> Customers { get; set; }
    }
}
