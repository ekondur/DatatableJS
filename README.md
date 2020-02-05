# EFDatatable
[![Build status](https://ci.appveyor.com/api/projects/status/jbdovlrhbm7041jd/branch/master?svg=true)](https://ci.appveyor.com/project/ekondur/efdatatable/branch/master)
[![NuGet](http://img.shields.io/nuget/v/EFDatatable.svg)](https://www.nuget.org/packages/EFDatatable/)
[![Gitter](https://badges.gitter.im/EFDatatable/community.svg)](https://gitter.im/EFDatatable/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge)
### What is EFDatatable?
EFDatatable is a helper to create a grid with Jquery Datatable and provides an extension to retrive data generically from Entity Framework context. It possible to use many datatable.js features with Html helper. It gives serverside or client side options.

```csharp
@(Html.EF().GridFor<Person>()
        .Name("PersonGrid")
        .Captions("top caption")
        .Paging(true)
        .Columns(cols =>
        {
            cols.Field(a => a.Id).Orderable(false).Searchable(true).Class("text-danger");
            cols.Field(a => a.Name).Title("First Name").Searchable(true);
            cols.Field(a => a.Age).Title("Age").Searchable(true);
            cols.Field(a => a.IsActive).Title("Active").Template("(data === true) ? '<span class=\"glyphicon glyphicon-ok\"></span>' : '<span class=\"glyphicon glyphicon-remove\"></span>'");
            cols.Field(a => a.BirthDate).Title("Birth Date").Format("DD-MMM-Y");
            cols.Command(a => a.Id, "onClick", text: "Click").Title("");
        })
        .Filters(filter =>
        {
            filter.Add(a => a.Id).GreaterThanOrEqual(1);
        })
        .Data("addParam")
        .URL(Url.Action("GetDataResult"), "POST")
        .ServerSide(true)
        .Render()
)
```
Add additional data to send action.
```javascript
<script>
    function addParam() {
        return { Param1: "test1", Param2: true, Param3: 5 };
    }

    function onClick(e) {
        alert(e);
    }
</script>
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
Install [EFDatatable](https://www.nuget.org/packages/EFDatatable/) from the package manager console:

```
PM> Install-Package EFDatatable
```

Then add [datatables.net](https://datatables.net/) Javascript and CSS files or links to your project. 

```html
<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.19/css/jquery.dataTables.css">
<script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.js"></script>
```
