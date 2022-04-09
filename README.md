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

## Tag helpers:
### Active link
This helper can replace the asp-action/asp-controller html attributes for url generation (when placed on A tag) and provides the capability to append or overwrite the class attribute when the link is active.
#### Configuration
Configuration can be done during startup with `SetPluginManagerOptions`
#### ActiveLinkOptions
Options | Default Value              | Description
------- |----------------------------| -----------
`DefaultAction` | "Index"                    | The default action to use when not provided
`DefaultActiveCssClass` | "active"                        | Active css class to use by default
`DefaultCssAction` | ActiveLinkCssClassAction.Append | The default action on the class attribute (replace/append)

#### Html Attributes:
Options | Description
------- | -----------
`active-link-controller` | Target controller
`active-link-action` | Target action
`active-link-area` | Target area
`active-link-route-*` | Additional route data
`active-link-css-class-action` | Css class action override when other then default (see ActiveLinkCssClassAction)
`active-link-css-class-active` | Css class to use as active marker

#### Example
```html5
<a class="nav-link text-dark" active-link-route-id="1" active-link-controller="Home" active-link-action="Privacy">Privacy</a>
```

## Plugins:
- [Breadcrumb](CorePluginManager.Breadcrumb/README.md)
- [Alert](CorePluginManager.Alert/README.md)
