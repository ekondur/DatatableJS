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

Then add [datatables.net](https://datatables.net/) Javascript and CSS files or links to your project. 

```html
<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.19/css/jquery.dataTables.css">
<script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.js"></script>
```
## URL
Set the URL to retrieve data from any action with GET or POST method: 

```csharp
.URL(Url.Action("GetAll", "Person"), "POST")
```
Use `DataRequest` object to bind parameters of request. With `.ToDataResult(request);` extension method, if there is entity framework, execute IQueryable list server side from context. Otherwise manipulate data manually and return `DataResult<T>` object including data.

Install [DatatableJS.Data](https://www.nuget.org/packages/DatatableJS.Data/) from the package manager console:
```
PM> Install-Package DatatableJS.Data
```
```csharp
using DatatableJS.Data;

public JsonResult GetAll(DataRequest request)
{
     DataResult<Person> result = ctx.People.ToDataResult(request);
     return Json(result);
}
``` 
## ServerSide
Enable server-side processing mode. By default DataTables operates in client-side processing mode. [Reference:](https://datatables.net/reference/option/serverSide)
```csharp
.ServerSide(true)
```
## Data
Pass additional parameters with `.Data("addParam")` method and add script function to view.
```javascript
function addParam() {
   return { Param1: "test1", Param2: true, Param3: 5 };
}
```
Get additional parameters with names.
```csharp
public JsonResult GetAll(DataRequest request, string Param1, bool Param2, int Param3)
{
     //
}
```
Or get parameters with object reference named **"data"**.
```csharp
public JsonResult GetAll(DataRequest request, AddData data)
{
     //
}
```
## Filters
Add additional filters before render datatable object. These filters are included in `.ToDataResult(request)` method with request while server side executing.
```csharp
.Filters(filter =>
{
    filter.Add(a => a.BirthDate).LessThanOrEqual(DateTime.Now);
    filter.Add(a => a.Age).NotEqual(25);
})
```
## Orders
Using this method you can define which column(s) the order is performed upon, and the ordering direction. [Reference](https://datatables.net/reference/option/order)
```csharp
.Orders(order => {
    order.Add(p => p.Name, OrderBy.Descending);
    order.Add(p => p.Age, OrderBy.Ascending);
})
```
## Length Menu
This method allows you to readily specify the entries in the length drop down _select_ list that DataTables shows . [Reference](https://datatables.net/reference/option/lengthMenu)
```csharp
.LengthMenu(new int[] {5,10,15})
```
Set _hasAll_ paramter true to show All option in select.
```csharp
.LengthMenu(new int[] {5,10,15}, true)
 ```
Set _allText_ paramter to change name of option All.
```csharp
.LengthMenu(new int[] {5,10,15}, true, "All Pages")
```
## Page Length
Number of rows to display on a single page when using pagination. [Reference](https://datatables.net/reference/option/pageLength)
```csharp
.PageLength(15)
```
## Name
Calling datatable on client-side is possible with name: 
```csharp
.Name("GridName")
```
Default name is "DataGrid". If there are multiple grid in single page, different names should be given. Call grid if you need like this:
```javascript
$(document).ready(function() {
      var table = $('#DataGrid').DataTable();
} );
```
## Searching
This option allows the search abilities of DataTables to be enabled or disabled. Default is "true". [Reference:](https://datatables.net/reference/option/searching)
```csharp
.Searching(false)
```
## Ordering
Enable or disable ordering of columns - it is as simple as that! DataTables, by default, allows end users to click on the header cell for each column, ordering the table by the data in that column. Default is "true". [Reference:](https://datatables.net/reference/option/ordering)
```csharp
.Ordering(false)
```
## Paging
DataTables can split the rows in tables into individual pages, which is an efficient method of showing a large number of records in a small space. The end user is provided with controls to request the display of different data as the navigate through the data. Default is "true". [Reference:](https://datatables.net/reference/option/paging)
```csharp
.Paging(false)
```
## Processing
Enable or disable the display of a 'processing' indicator when the table is being processed. Default is true. [Reference:](https://datatables.net/reference/option/processing)
```csharp
.Processing(false)
```
## ScrollX
Enable horizontal scrolling. When a table is too wide to fit into a certain layout, or you have a large number of columns in the table, you can enable horizontal (x) scrolling to show the table in a viewport, which can be scrolled. [Reference:](https://datatables.net/reference/option/scrollX)
```csharp
.ScrollX(true)
```
## Class
Default table css class is `"display nowrap dataTable dtr-inline collapsed"`. It can be replaced with other table class like bootstrap "table table-striped".
```csharp
.Class("table table-striped")
```
## Captions
Adding some text on table header or footer with Captions method that:

```csharp
.Captions("Top caption text...", "Bottom caption text...")
```
## Fixed Columns
FixedColumns provides the option to freeze one or more columns to the left or right of a horizontally scrolling DataTable. [Reference:](https://datatables.net/reference/option/fixedColumns)
```csharp
.FixedColumns(leftColumns: 1, rightColumns: 3)
```
## Individual Column Searching
With this feature, data is be searchable column by column if column searchable is not false.

```csharp
.ColumnSearching(true)
```
To give class for these inputs:

```csharp
.ColumnSearching(true, "form-control")
```
## Title
Set column header. Default is property name.
```
.Title("Person Name");
```
Or use `DisplayAttribute` for properties.
```csharp
[Display(Name = "Person Name")]
public string Name { get; set; }
```
## Format
Customize datetime format with [Moment.js](https://momentjs.com/) expression.
```csharp
.Format("DD-MMM-Y");
```
Or use `DisplayFormatAttribute` for properties.
```csharp
[DisplayFormat(DataFormatString = "DD-MMM-Y")]
public DateTime? BirthDate { get; set; }
```
## Template
Manipulate and change display of column according to data.
```csharp
.Template("(data === true) ? '<span class=\"glyphicon glyphicon-ok\"></span>' : '<span class=\"glyphicon glyphicon-remove\"></span>'");
```
## Visible
Set column visible or hidden, default is `true`.
## Searchable
Set column searchable or not, default is `true`.
## Orderable
Set column orderable or not, default is `true`.
## Width
Set column width percentage.
## Class
Set css class of column.
## DefaultContent
Set default value for null data.
## Command
Add column commands to table in a variety of ways.
```csharp
cols.Command(a => a.Name, "onClick").Title("Link");
cols.Command(a => a.Name, "onClick", "Click").Title("Link");
cols.Command(a => a.Id, "onClick", "Click", "glyphicon glyphicon-edit").Title("Edit");
cols.Command(a => a.Id, "onClick", "Click", "glyphicon glyphicon-edit", "btn btn-danger btn-xs").Title("Edit");
```
```javascript
function onClick(e) {
   alert(e);
}
```
## Commands
Add button groups as a commands.
```csharp
cols.Commands(a => a.Id, new[] { new Command("Update", "onUpdate"), new Command("Delete", "onDelete") }, "Reports").Title("Actions");
cols.Commands(a => a.Id, new[] { new Command("Excel", "onDelete"), new Command("Pdf", "onUpdate") }, "Reports", "", "", "glyphicon glyphicon-export").Title("Export");
```
Default language is English. You can change with other languages or custumize it. Set json url parameter to Language method for other languages from [CDN](https://datatables.net/plug-ins/i18n/).

```csharp
.Language("https://cdn.datatables.net/plug-ins/1.10.20/i18n/Turkish.json")
```
Or add json file local and customize it.

```csharp
.Language(Url.Content("/Scripts/Turkish.json"))
```
Json example is below:

```json
{
    "sEmptyTable":     "No data available in table",
    "sInfo":           "Showing _START_ to _END_ of _TOTAL_ entries",
    "sInfoEmpty":      "Showing 0 to 0 of 0 entries",
    "sInfoFiltered":   "(filtered from _MAX_ total entries)",
    "sInfoPostFix":    "",
    "sInfoThousands":  ",",
    "sLengthMenu":     "Show _MENU_ entries",
    "sLoadingRecords": "Loading...",
    "sProcessing":     "Processing...",
    "sSearch":         "Search:",
    "sZeroRecords":    "No matching records found",
    "oPaginate": {
        "sFirst":    "First",
        "sLast":     "Last",
        "sNext":     "Next",
        "sPrevious": "Previous"
    },
    "oAria": {
        "sSortAscending":  ": activate to sort column ascending",
        "sSortDescending": ": activate to sort column descending"
    }
}
```
