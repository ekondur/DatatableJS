using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DatatableJS.Data.UnitTest
{
    [TestFixture]
    public class Tests
    {
        IQueryable<Person> _list;
        DataRequest _request;

        [SetUp]
        public void Setup()
        {
            _list = new List<Person>
            {
                new Person { Name = "Jon" },
                new Person { Name = "Arya" }
            }.AsQueryable();

            var _columns = new List<Column> {
                new Column {data = "Name", name = "Name", orderable = true, searchable = true, search = new Search()}
            };

            _request = new DataRequest { columns = _columns, draw = 1, length = 10 };
        }

        [Test]
        public void ToDataResult_WhenFilterWithNameDoesNotContain_ReturnsListCountZero()
        {
            _request.filters.Add(new Filter { Field = "Name", Operand = Operand.Contains, Value = "Jon" });
            var result = _list.ToDataResult(_request);

            Assert.That(result.data.Count, Is.EqualTo(1));
        }
    }

    public class Person
    {
        public string Name { get; set; }
    }
}