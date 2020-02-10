#### Basic Configuration
Define using EFDatatable in view or web config global for all views.

```Razor
@using EFDatatable;

@(Html.EF().GridFor<Person>()
    .Name("PersonGrid")
    .Columns(cols =>
    {
        cols.Field(a => a.Id).Visible(false);
        cols.Field(a => a.Name).Title("First Name");
        cols.Field(a => a.Age).Title("Age");
    })
    .URL(Url.Action("GetDataResult"), "POST")
    .ServerSide(true)
    .Render()
)
```
Execute data server side with ```ToDataResult(request)``` method simply or manipulate your data and return ```DataResult<T>```  object includes data.

```csharp
using EFDatatable.Data;

public JsonResult GetDataResult(DataRequest request)
{
    DataResult<Person> result = ctx.People.ToDataResult(request);
    return Json(result);
}
```
```DataRequest``` ojbect includes datatable parameters like ```start,length,columns```

Previous: [Installation](https://github.com/ekondur/EFDatatable/blob/master/EFDatatable.Web/Docs/Installation.md)