using EFDatatable.Net.Models;
using System.Data.Entity;

namespace EFDatatable.Net.Sql
{
    public class EFContext : DbContext
    {
        public EFContext()
            : base("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EFDb;Integrated Security=true;MultipleActiveResultSets=true")
        {

        }

        public DbSet<Person> People { get; set; }
    }
}