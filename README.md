# DatatableJS
[![Build status](https://ci.appveyor.com/api/projects/status/jquswi3vnm0kd9n9/branch/master?svg=true)](https://ci.appveyor.com/project/ekondur/datatablejs/branch/master)
[![NuGet](http://img.shields.io/nuget/v/DatatableJS.svg)](https://www.nuget.org/packages/DatatableJS/)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/d794ab84939f4ddc96fffebd6ddf6ff7)](https://www.codacy.com/gh/ekondur/DatatableJS/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=ekondur/DatatableJS&amp;utm_campaign=Badge_Grade)
[![Gitter](https://badges.gitter.im/DatatableJS/community.svg)](https://gitter.im/DatatableJS/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge)
### What is DatatableJS?
DatatableJS is a helper to create a grid with Jquery Datatable and provides an extension to retrive data generically from Entity Framework context. It possible to use many datatable.js features with Html helper. It gives serverside or client side options. There's more: [Wiki Documentation](https://github.com/ekondur/DatatableJS/wiki)

### Where can I get it?
Install [DatatableJS.Net](https://www.nuget.org/packages/DatatableJS.Net/) for .Net Frameworks (4.5...4.8) from the package manager console:

```
PM> Install-Package DatatableJS.Net
```

```csharp
@(Html.JS().Datatable<Person>()
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
Or, Install [DatatableJS](https://www.nuget.org/packages/DatatableJS/) for .Net Core, .Net 5, .Net 6 and use tag helpers.

```
PM> Install-Package DatatableJS
```

```html
<datatable name="PersonGrid">
    <columns>
        <column field="Id" visible="false" />
        <column field="Name" width="50" title="Full Name" />
        <column field="Age" />
        <command-item field="Id" on-click="onClick" btn-class="btn btn-info" text="Edit" icon-class="fa fa-edit"/>
        <commands field="Id" text="Actions" items='new [] { new Command("Update", "onClick"), new Command("Delete", "onClick") }'/>
    </columns>
    <data-source url="@Url.Action("GetDataResult")" method="POST" server-side="true" data="addParam"/>
    <language url="//cdn.datatables.net/plug-ins/1.10.22/i18n/Turkish.json"/>
    <filters>
        <add field="Id" value="1" operand="GreaterThanOrEqual"/>
    </filters>
    <captions top="top caption here" bottom="bottom caption here"/>
    <render />
</datatable>
```

With "ToDataResult(request)" extension function, data can get with server side pagination very simply. To use this feature
install [DatatableJS.Data](https://www.nuget.org/packages/DatatableJS.Data/) from the package manager console:
```
PM> Install-Package DatatableJS.Data
```
```csharp
using DatatableJS.Data

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
