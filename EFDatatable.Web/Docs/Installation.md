### Getting Started
#### Installation
Install the EFDatatable packages from nuget,
```
PM> Install-Package EFDatatable
```
Then add datatable.js script link to layout file after jquery scripts,
```html
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.19/css/jquery.dataTables.css">
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.js"></script>
```
To format DateTime columns also need to add moment scripts,
```html
    <script src="//cdnjs.cloudflare.com/ajax/libs/moment.js/2.11.2/moment.min.js"></script>
    <script src="//cdn.datatables.net/plug-ins/1.10.12/sorting/datetime-moment.js"></script>
```
Next: [Basic Configuration](https://github.com/ekondur/EFDatatable/blob/master/EFDatatable.Web/Docs/Basic-Configuration.md)