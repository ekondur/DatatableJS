namespace EFDatatable.Sql.Migrations
{
    using EFDatatable.Sql.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EFContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(EFContext context)
        {
            //  This method will be called after migrating to the latest version.
            using (var ctx = new EFContext())
            {
                ctx.Customers.AddOrUpdate(new Customer { Id = 1, Name = "John Snow" });
                ctx.Customers.AddOrUpdate(new Customer { Id = 1, Name = "Arya Stark" });
                ctx.SaveChanges();
            }
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
