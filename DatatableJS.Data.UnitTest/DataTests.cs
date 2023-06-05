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
                new Person { Name = "Jon", Age = 1},
                new Person { Name = "Arya", Age = 1 },
                new Person { Name = "Arya", Age = 2 }
            }.AsQueryable();

            var _columns = new List<Column> {
                new Column {data = "Name", name = "Name", orderable = true, searchable = true, search = new Search()},
                new Column {data = "Age", name = "Age", orderable = true, searchable = true, search = new Search()},
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

        [Test]
        public void ToDataResult_WhenFilterWithNameIgnoreCase_ReturnsListCountZero()
        {
            _request.filters.Add(new Filter { Field = "Name", Operand = Operand.Contains, Value = "jon", CaseSensitive = false});
            var result = _list.ToDataResult(_request);

            Assert.That(result.data.Count, Is.EqualTo(1));
        }

        [Test]
        public void ToDataResult_WhenFilterWithNameCaseSensitive_ReturnsListCountZero()
        {
            _request.filters.Add(new Filter { Field = "Name", Operand = Operand.Contains, Value = "jon", CaseSensitive = true });
            var result = _list.ToDataResult(_request);
            Assert.That(result.data.Count, Is.EqualTo(0));
            _request.filters.Add(new Filter { Field = "Name", Operand = Operand.Contains, Value = "Jon", CaseSensitive = true });
            result = _list.ToDataResult(_request);
            Assert.That(result.data.Count, Is.EqualTo(1));
        }

        [Test]
        public void ToDataResult_WhenSortingByNameAndAge()
        {
            _request.order.Add(new Order
            {
                column = 0,
                dir = "asc"
            });
            _request.order.Add(new Order
            {
                column = 1,
                dir = "asc"
            });
            var result = _list.ToDataResult(_request);

            Assert.That(result.data[0].Name, Is.EqualTo("Arya"));
            Assert.That(result.data[0].Age, Is.EqualTo(1));
            Assert.That(result.data[1].Name, Is.EqualTo("Arya"));
            Assert.That(result.data[1].Age, Is.EqualTo(2));
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}