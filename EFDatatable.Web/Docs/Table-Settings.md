####Table Settings
Configuring Jquery datatable settings with helper is very easy.
- [Name](#name)
- [Searching](#searching)
- [Ordering](#ordering)
#####Name
To call datatable on clinet side is possible with name  ```.Name("GridName")```. If there are multiple grid in single page, different names should be given. Call grid like this:
```javascript
$(document).ready(function() {
      var table = $('#GridName').DataTable();
} );
```
#####Searching
Searching setting is true by default. If needs to disable searching for all columns, set ```.Searching(false)``` then also search bar is disappeared.
#####Ordering
If needs to disable ordering for all columns, set ```.Ordering(false)```.
