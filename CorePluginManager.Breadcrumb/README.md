# Breadcrumbs

## Installation
In your `_ViewImports.cshtml` add the tag helpers with:
`@addTagHelper *, CorePluginManager.Breadcrumb`

### Configuration
Configuration can be done during startup with `SetPluginManagerOptions`
### BreadcrumbOptions
Options | Default Value  | Description
------- |----------------| -----------
`DefaultAction` | "Index"        | Action to use when non is set in BreadcrumbItem
`ActiveCssClass` | "active"       | Css class for the active breadcrumb item
`OlCssClass` | "breadcrumb"   |  
`LiCssClass` | "breadcrumb-item" | 

### Usage
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

### Properties : IBreadcrumbService
Property  | Description
------------- |---------------
`List<BreadcrumbItem> Items { get; }` | Retrieve current list of breadcrumb items

### Methods : IBreadcrumbService
Method  | Description
------------- |---------------
`void Add(BreadcrumbItem item)` | Add breadcrumb item

