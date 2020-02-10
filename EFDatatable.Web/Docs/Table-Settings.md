#### Table Settings
Configuring Jquery datatable settings with helper is very easy.
- [Name](#name)
- [Searching](#searching)
- [Ordering](#ordering)
- [Paging](#paging)
- [Class](#class)
##### Name
To call datatable on clinet side is possible with name  ```.Name("GridName")```. Default name is "DataGrid". If there are multiple grid in single page, different names should be given. Call grid like this:
```javascript
$(document).ready(function() {
      var table = $('#GridName').DataTable();
} );
```
##### Searching
Searching setting is true by default. If needs to disable searching for all columns, set ```.Searching(false)``` then also search bar is disappeared. Default is "true".
##### Ordering
If needs to disable ordering for all columns, set ```.Ordering(false)```. Default is "true".
##### Paging
If ```.Paging(false)``` setted then all data appears on table without pagination. Default is "true".
##### Class
Default table css is ```"display nowrap dataTable dtr-inline collapsed"```. It can be replaced with other table class like bootstrap "table table-striped"

Previous: [Basic Configuration](https://github.com/ekondur/EFDatatable/blob/master/EFDatatable.Web/Docs/Basic-Configuration.md)