using System;

namespace EFDatatable.Web.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short? Age { get; set; }
        public bool IsActive { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}