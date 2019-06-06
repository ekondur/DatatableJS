using EFDatatable.Net.Models;
using EFDatatable.Net.Sql;
using System.Data.Entity.Migrations;

namespace EFDatatable.Net.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<EFDatatable.Net.Sql.EFContext>
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
                ctx.People.AddOrUpdate(new Person { Id = 1, Name = "Jon Snow" });
                ctx.People.AddOrUpdate(new Person { Id = 2, Name = "Arya Stark" });
                ctx.People.AddOrUpdate(new Person { Id = 3, Name = "Daenerys Targaryen" });
                ctx.People.AddOrUpdate(new Person { Id = 4, Name = "Cersei Lannister" });
                ctx.People.AddOrUpdate(new Person { Id = 5, Name = "Bran Stark" });
                ctx.People.AddOrUpdate(new Person { Id = 6, Name = "Sansa Stark" });
                ctx.People.AddOrUpdate(new Person { Id = 7, Name = "Tyrion Lannister" });
                ctx.People.AddOrUpdate(new Person { Id = 8, Name = "Lord Varys" });
                ctx.People.AddOrUpdate(new Person { Id = 9, Name = "Jaime Lannister" });
                ctx.People.AddOrUpdate(new Person { Id = 10, Name = "Brienne of Tarth" });
                ctx.People.AddOrUpdate(new Person { Id = 11, Name = "Theon Greyjoy" });
                ctx.SaveChanges();
            }
        }
    }
}
