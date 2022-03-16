# Core-PluginManager
This is a personal project to combine multiple small libraries and pieces of code into one reusable package. This package then will also be usable to create additional 'plugins' in the future.

## Setup .NET 6
After WebApplication.CreateBuilder add:
```cs
builder.BuildPluginManager(typeof(Program).Assembly);
```

And directly after builder.Build add:
```cs
app.UsePluginManager();
```

In your `_ViewImports.cshtml` add the tag helpers with:
`@addTagHelper *, CorePluginManager`

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

#### Methods
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

### Breadcrumbs

#### Configuration


#### Usage

