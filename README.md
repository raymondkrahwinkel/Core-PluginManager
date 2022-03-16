# Core-PluginManager
This is a personal project to combine multiple small libraries and pieces of code into one reusable package. This package then will also be usable to create additional 'plugins' in the future.
Beneath here you can find the documentation created along the way or check the Demo project.

## Setup .NET 6
After WebApplication.CreateBuilder add:
```cs
builder.BuildPluginManager(typeof(Program).Assembly);
```

To set options for your plugin chain `SetPluginManagerOptions` after `BuildPluginManager` like this:
```cs 
builder
    .BuildPluginManager(typeof(Program).Assembly)
    .SetPluginManagerOptions(new BreadcrumbOptions() { ActiveCssClass = "active" })
```

And directly after builder.Build add:
```cs
app.UsePluginManager();
```

In your `_ViewImports.cshtml` add the tag helpers with:
`@addTagHelper *, CorePluginManager`

## PluginManager Class
### Properties
Property | Description
-------- | -----------
`static Assembly Parent` | Contains the assembly set during startup

### Methods
Method | Description
------ | -----------
`static T GetPluginManagerOptions<T>(defaultValue = default)` | Get your options set at startup, returns `defaultValue` when no options where set during startup

## Configuration
The plugin manager reads its configuration from the "PluginManager" section in the appsettings.json file.

```json 
"PluginManager": {
    "SessionIdleTimeout": 20,
    "SessionIOTimeout": 1
}
```

## Interfaces
Interface | Description
------------- |---------------
`IPluginApplicationBuilder` | Used to inject code at ApplicationBuilder level
`IPluginServiceCollection` | Used to inject code at the ServiceCollection level

## Helpers:
### SessionHelper
This helper is by default added as `AddTransient<ISessionHelper, SessionHelper>()` by the plugin manager.

#### Configuration
Configuration | Default value | Note
------------- |---------------| ----
`SessionIdleTimeout` | 20            | minutes
`SessionIOTimeout` | 1             | minutes

#### Methods : ISessionHelper
Method  | Description
------------- | -------------
`bool Exists(string key)`  | Checks if a key exists within a session
`bool Exists(string group, string key)`  | Checks if a key exists within a session group
`T? Get<T>(string key)`  | Retrieve data for key
`T? Get<T>(string group, string key)`  | Retrieve data for key within specified group
`void Set(string key, object payload)`  | Storage data in the session
`void Set(string group, string key, object payload)`  | Storage data in the session within a group
`void Delete(string key)`  | Delete data from session
`void Delete(string group, string key)`  | Delete data from session within a group

#### Notes
The session helper can create groups within the session, this can be used to better separate data. For example the alerts plugin stores all its data within the group 'alert'. 

## Plugins:
### Alerts
With this you can add error, info, success and warning messages via the service layer and render them after a page refresh.
#### Configuration


#### Usage
At the point where you want to render the alerts add `<alert></alert>` this will generate a default bootstrap 5 alert.
You can also use the `IAlertService` to get the stored messages and create your own alert view of course.

#### Methods : IAlertService
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

### Breadcrumbs

#### Configuration
Configuration can be done during startup with `SetPluginManagerOptions`
#### Available options
Options | Default Value  | Description
------- |----------------| -----------
`DefaultAction` | "Index"        | Action to use when non is set in BreadcrumbItem
`ActiveCssClass` | "active"       | Css class for the active breadcrumb item
`OlCssClass` | "breadcrumb"   |  
`LiCssClass` | "breadcrumb-item" | 

#### Usage
In your cshtml add `<breadcrumb></breadcrumb>` where you want to render the breadcrumb. And via the Dependency Injection the breadcrumb service is available as `IBreadcrumbService`
You can use the `[Breadcrumb]` attribute above your controller classes and methods with return type IActionResult, like:
```cs
[Breadcrumb("Test page")]
public IActionResult Index()
{
    return View();
}
```

You can also set 1 default breadcrumb item that will always be at the start of your breadcrumb path, you can do this with `[DefaultBreadcrumb]` like:
```cs
[DefaultBreadcrumb("Home")]
public IActionResult Index()
{
    return View();
}
```

And via the `IBreadcrumbService.Add()` you can add custom breadcrumb items in your code.

#### Properties : IBreadcrumbService
Property  | Description
------------- |---------------
`List<BreadcrumbItem> Items { get; }` | Retrieve current list of breadcrumb items

#### Methods : IBreadcrumbService
Method  | Description
------------- |---------------
`void Add(BreadcrumbItem item)` | Add breadcrumb item

