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
If property names are pascal case like "FirstName" and JSON naming policy is camel case like "firstName", enable it.
```csharp
.URL(Url.Action("GetDataResult"), "POST", camelCase: true)
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
## Selecting
Select adds item selection capabilities to a DataTable. Items can be rows, columns or cells, which can be selected independently, or together.  [Reference:](https://datatables.net/extensions/select/)
```csharp
.Selecting(true)
.Selecting(true, SelectItems.Checkbox)
.Selecting(true, SelectItems.Cell, SelectStyle.Single)
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

```csharp
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
```csharp
.Visible(false);
```
## Searchable
Set column searchable or not, default is `true`.
```csharp
.Searchable(false);
```
## Orderable
Set column orderable or not, default is `true`.
```csharp
.Orderable(false);
```
## Width
Set column width percentage.
```csharp
.Width(50);
```
## Class
Set css class of column.
```csharp
.Class("text-danger");
```
## DefaultContent
Set default value for null data.
```csharp
.DefaultContent("No Data");
```
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
## Localizations
Default language is English. You can change with other languages or custumize it. Set json url parameter to Language method for other languages from [CDN](https://datatables.net/plug-ins/i18n/).

```csharp
.Language("https://cdn.datatables.net/plug-ins/1.10.20/i18n/Turkish.json")
```
Or add json file local and customize it. [Reference:](https://datatables.net/reference/option/language)

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
Or, you can just change what you want with builder;
```csharp
.Language(lang => lang.EmptyTable("No Data").Search("Search by: "))
```
- **Decimal**: Set the decimal place character. [Reference:](https://datatables.net/reference/option/language.decimal)
- **EmptyTable**: This string is shown when the table is empty of data (regardless of filtering). [Reference:](https://datatables.net/reference/option/language.emptyTable)
- **Info**: This string gives information to the end user about the information that is current on display on the page. [Reference:](https://datatables.net/reference/option/language.info)
- **InfoEmpty**: Display information string for when the table is empty. [Reference:](https://datatables.net/reference/option/language.infoEmpty)
- **InfoFiltered**: When a user filters the information in a table, this string is appended to the information (info) to give an idea of how strong the filtering is. [Reference:](https://datatables.net/reference/option/language.infoFiltered)
- **InfoPostFix**: If can be useful to append extra information to the info string at times, and this variable does exactly that. [Reference:](https://datatables.net/reference/option/language.infoPostFix)
- **Thousands**: The thousands separator option is used for output of information only. [Reference:](https://datatables.net/reference/option/language.thousands)
- **LengthMenu**: Detail the action that will be taken when the drop down menu for the pagination length option is changed. [Reference:](https://datatables.net/reference/option/language.lengthMenu)
- **LoadingRecords**: This message is shown in an empty row in the table to indicate to the end user the the data is being loaded. [Reference:](https://datatables.net/reference/option/language.loadingRecords)
- **Processing**: Text that is displayed when the table is processing a user action (usually a sort command or similar). [Reference:](https://datatables.net/reference/option/language.processing)
- **Search**: Sets the string that is used for DataTables filtering input control. [Reference:](https://datatables.net/reference/option/language.search)
- **ZeroRecords**: Text shown inside the table records when the is no information to be displayed after filtering. [Reference:](https://datatables.net/reference/option/language.zeroRecords)

## Callbacks
Jquery datatable supports many callback functionalities:
- **createdRow**: Callback for whenever a TR element is created for the table's body. [Reference:](https://datatables.net/reference/option/createdRow)
- **drawCallback**: Function that is called every time DataTables performs a draw. [Reference:](https://datatables.net/reference/option/drawCallback)
- **footerCallback**: Footer display callback function. [Reference:](https://datatables.net/reference/option/footerCallback)
- **formatNumber**: Number formatting callback function. [Reference:](https://datatables.net/reference/option/formatNumber)
- **headerCallback**: Header display callback function. [Reference:](https://datatables.net/reference/option/headerCallback)
- **infoCallback**: Table summary information display callback. [Reference:](https://datatables.net/reference/option/infoCallback)
- **initComplete**: Initialisation complete callback. [Reference:](https://datatables.net/reference/option/initComplete)
- **preDrawCallback**: Pre-draw callback. [Reference:](https://datatables.net/reference/option/preDrawCallback)
- **rowCallback**: Row draw callback. [Reference:](https://datatables.net/reference/option/rowCallback)
- **stateLoadCallback**: Callback that defines where and how a saved state should be loaded. [Reference:](https://datatables.net/reference/option/stateLoadCallback)
- **stateLoadParams**: State loaded - data manipulation callback. [Reference:](https://datatables.net/reference/option/stateLoadParams)
- **stateLoaded**: State loaded callback. [Reference:](https://datatables.net/reference/option/stateLoaded)
- **stateSaveCallback**: Callback that defines how the table state is stored and where. [Reference:](https://datatables.net/reference/option/stateSaveCallback)
- **stateSaveParams**: State save - data manipulation callback [Reference:](https://datatables.net/reference/option/stateSaveParams)
 
 You can easily define them with helper:
 ```csharp
.Callbacks(x => x.CreatedRow("createdRow"))
```
 ```javascript
function createdRow(row, data, dataIndex, cells){
    console.log(dataIndex + " " + data.Name);
}
 ```
 Or multiple callback functions:
  ```csharp
 .Callbacks(x => x.CreatedRow("createdRow").InitComplete("initComplete"))
 ```
### ColReorder:
ColReorder adds the ability for the end user to be able to reorder columns in a DataTable through a click-and-drag operation. This can be useful when presenting data in a table, letting the user move columns that they wish to compare next to each other for easier comparison.
```csharp
.ColReorder(true)
```
Or, configure it; [Reference:](https://datatables.net/reference/option/#colreorder)
* Initial enablement state of ColReorder.
```csharp
.ColReorder(x => x.Enable(false))
```
* Disallow x columns from reordering (counting from the left).
```csharp
.ColReorder(x => x.FixedColumnsLeft(1))
```
* Disallow x columns from reordering (counting from the right).
```csharp
.ColReorder(x => x.FixedColumnsRight(1))
```
* Set a default order for the columns in the table.
```csharp
.ColReorder(x => x.Order(5,4,3,2,1,0))
```
* Enable/Disable live reordering of columns during a drag.
```csharp
.ColReorder(x => x.RealTime(false))
```
