# EFDatatable
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/82fb18925fdd49a7bac10a18019c254f)](https://app.codacy.com/gh/ekondur/EFDatatable?utm_source=github.com&utm_medium=referral&utm_content=ekondur/EFDatatable&utm_campaign=Badge_Grade)
[![Build status](https://ci.appveyor.com/api/projects/status/jbdovlrhbm7041jd/branch/master?svg=true)](https://ci.appveyor.com/project/ekondur/efdatatable/branch/master)
[![NuGet](http://img.shields.io/nuget/v/EFDatatable.svg)](https://www.nuget.org/packages/EFDatatable/)
[![Gitter](https://badges.gitter.im/EFDatatable/community.svg)](https://gitter.im/EFDatatable/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge)
### What is EFDatatable?
EFDatatable is a helper to create a grid with Jquery Datatable and provides an extension to retrive data generically from Entity Framework context. It possible to use many datatable.js features with Html helper. It gives serverside or client side options. There's more: [Wiki Documentation](https://github.com/ekondur/EFDatatable/wiki)

### Where can I get it?
Install [EFDatatable](https://www.nuget.org/packages/EFDatatable/) from the package manager console:

```
PM> Install-Package EFDatatable
```

```csharp
@(Html.EF().Datatable<Person>()
        .Name("PersonGrid")
        .Columns(cols =>
        {
            cols.Field(a => a.Id).Visible(false);
            cols.Field(a => a.Name).Title("First Name").Class("text-danger");
            cols.Field(a => a.Age).Title("Age").Searchable(false);
            cols.Field(a => a.BirthDate).Title("Birth Date").Format("DD-MMM-Y");
            cols.Command(a => a.Id, "onClick", text: "Click").Title("");
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
If data layer in a different project install [EFDatatable.Data](https://www.nuget.org/packages/EFDatatable.Data/) from the package manager console:
```
PM> Install-Package EFDatatable.Data
```
```csharp
using EFDatatable.Data

public JsonResult GetDataResult(DataRequest request)
{
    DataResult result = context.People.ToDataResult(request);
    return Json(result);
}
```

Then add [datatables.net](https://datatables.net/) Javascript and CSS files or links to your project. 

```html
<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.19/css/jquery.dataTables.css">
<script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.js"></script>
```
