# DatatableJS
[![Build status](https://ci.appveyor.com/api/projects/status/jquswi3vnm0kd9n9/branch/main?svg=true)](https://ci.appveyor.com/project/ekondur/datatablejs/branch/main)
[![NuGet](http://img.shields.io/nuget/v/DatatableJS.svg)](https://www.nuget.org/packages/DatatableJS/)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/d794ab84939f4ddc96fffebd6ddf6ff7)](https://www.codacy.com/gh/ekondur/DatatableJS/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=ekondur/DatatableJS&amp;utm_campaign=Badge_Grade)
[![Gitter](https://badges.gitter.im/DatatableJS/community.svg)](https://gitter.im/DatatableJS/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge)
### What is DatatableJS?
DatatableJS is a helper to create a grid with Jquery Datatable and provides an extension to retrive data generically from Entity Framework context. It possible to use many datatable.js features with Html helper. It gives serverside or client side options. There's more: [Wiki Documentation](https://github.com/ekondur/DatatableJS/wiki)

![Ekran Alıntısı](https://user-images.githubusercontent.com/4971326/87161335-0f5c1a80-c2cd-11ea-8e19-ed43087bbbe5.PNG)

### Where can I get it?
Install [DatatableJS](https://www.nuget.org/packages/DatatableJS/) for .Net Core, .Net 5, .Net 6 and use tag helpers.

```
PM> Install-Package DatatableJS
```

```csharp
@(Html.JS().Datatable<Person>()
        .Name("PersonGrid")
        .Columns(col =>
        {
            col.Field(a => a.Id).Visible(false);
            col.Field(a => a.Name).Title("First Name").Class("text-danger");
            col.Field(a => a.Age).Title("Age").Searchable(false);
            col.Field(a => a.BirthDate).Title("Birth Date").Format("DD-MMM-Y");
            col.Command(a => a.Id, "onClick", text: "Click").Title("");
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
Or, use tag helper:

```html
<js-datatable name="PersonGrid">
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
</js-datatable>
```
To use on .Net Frameworks (4.5 ... 4.8) Install [DatatableJS.Net](https://www.nuget.org/packages/DatatableJS.Net/) from the package manager console:

```
PM> Install-Package DatatableJS.Net
```

Using `.ToDataResult(request)` extension function with IQueryable collection, provides data can get with server side pagination very simply. To use this feature
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
