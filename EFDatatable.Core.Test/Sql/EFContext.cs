using EFDatatable.Core.Test.Models;
using Microsoft.EntityFrameworkCore;

namespace EFDatatable.Core.Test.Sql
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions<EFContext> options)
            : base(options)
        {

        }
        public DbSet<Person> People { get; set; }
    }
}
