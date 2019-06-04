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
                ctx.Customers.AddOrUpdate(new Customer { Id = 1, Name = "Jon Snow" });
                ctx.Customers.AddOrUpdate(new Customer { Id = 2, Name = "Arya Stark" });
                ctx.Customers.AddOrUpdate(new Customer { Id = 3, Name = "Daenerys Targaryen" });
                ctx.Customers.AddOrUpdate(new Customer { Id = 4, Name = "Cersei Lannister" });
                ctx.Customers.AddOrUpdate(new Customer { Id = 5, Name = "Bran Stark" });
                ctx.Customers.AddOrUpdate(new Customer { Id = 6, Name = "Sansa Stark" });
                ctx.Customers.AddOrUpdate(new Customer { Id = 7, Name = "Tyrion Lannister" });
                ctx.Customers.AddOrUpdate(new Customer { Id = 8, Name = "Lord Varys" });
                ctx.Customers.AddOrUpdate(new Customer { Id = 9, Name = "Jaime Lannister" });
                ctx.Customers.AddOrUpdate(new Customer { Id = 10, Name = "Brienne of Tarth" });
                ctx.Customers.AddOrUpdate(new Customer { Id = 11, Name = "Theon Greyjoy" });
                ctx.SaveChanges();
            }
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
