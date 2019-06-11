# EFDatatable
### What is EFDatatable?
EFDatatable is a helper to create a grid with Jquery Datatable and provides an extension to retrive data generically from Entity Framework context. It possible to use many datatable.js features with Html helper. It gives serverside or client side options.
```csharp
@(Html.EF().GridFor<Person>()
        .Name("PersonGrid")
        .Searching(true)
        .Class("table table-striped")
        .Columns(cols =>
        {
            cols.Field(a => a.Id).Visible(true).Orderable(false).Searchable(true);
            cols.Field(a => a.Name).Title("First Name");
        })
        .Filters(filter =>
        {
            filter.Add(a => a.Id).GreaterThanOrEqual(1);
        })
        .URL(Url.Action("GetDataResult"), "POST")
        .ServerSide(true)
        .Render()
)
```
With "ToDataResult(request)" extension function, data can get with server side pagination very simply.
```csharp
public JsonResult GetDataResult(DataRequest request)
    {
        DataResult result = context.People.ToDataResult(request);
        return Json(result);
    }
```
### Where can I get it?
First, [install NuGet](http://docs.nuget.org/docs/start-here/installing-nuget). Then, install [EFDatatable](https://www.nuget.org/packages/EFDatatable/) from the package manager console:

```
PM> Install-Package EFDatatable
```
