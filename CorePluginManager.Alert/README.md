# Alerts
With this you can add error, info, success and warning messages via the service layer and render them after a page refresh.

## Installation
In your `_ViewImports.cshtml` add the tag helpers with:
`@addTagHelper *, CorePluginManager.Alert`

### Configuration
Configuration can be done during startup with `SetPluginManagerOptions`
### AlertOptions
Options | Default Value                 | Description
------- |-------------------------------| -----------
`DismissibleCssClass` | "alert-dismissible fade show" | Css class string to use for dismissible alerts
`ErrorMessages` | CssClass = "alert-danger"     | See below AlertOptions.MessageOptions
`WarningMessages` | CssClass = "alert-warning"    | See below AlertOptions.MessageOptions
`SuccessMessages` | CssClass = "alert-success"    | See below AlertOptions.MessageOptions
`InfoMessages` | CssClass = "alert-info"          | See below AlertOptions.MessageOptions

### AlertOptions.MessageOptions
Options | Default Value                 | Description
------- |-------------------------------| -----------
`CssClass` | "" | Css class for the alert box
`Dismissible` | true | Enable close button on message

### Usage
At the point where you want to render the alerts add `<alert></alert>` this will generate a default bootstrap 5 alert.
You can also use the `IAlertService` to get the stored messages and create your own alert view of course.

### Methods : IAlertService
Method  | Description
------------- |---------------
`void Error(string message)` | Add error message
`void Success(string message)` | Add success message
`void Warning(string message)` | Add warning message
`void Infor(string message)` | Add info message
`List<string> GetErrorMessages()` | Get all stored error messages and deletes them from storage
`List<string> GetSuccessMessages()` | Get all stored success messages and deletes them from storage
`List<string> GetWarningMessages()` | Get all stored warning messages and deletes them from storage
`List<string> GetInfoMessages()` | Get all stored info messages and deletes them from storage