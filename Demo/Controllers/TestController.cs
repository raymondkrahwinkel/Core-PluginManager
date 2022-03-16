using CorePluginManager.Plugins.Breadcrumb;
using CorePluginManager.Plugins.Breadcrumb.Attributes;
using CorePluginManager.Plugins.Breadcrumb.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers;

[Breadcrumb("Test class")]
// [Area("AreaName")]
public class TestController : Controller
{
    private readonly IBreadcrumbService _breadcrumbService;

    public TestController(IBreadcrumbService breadcrumbService)
    {
        _breadcrumbService = breadcrumbService;
    }
    
    [Breadcrumb("Test page")]
    public IActionResult Index()
    {
        return View();
    }
    
    [Breadcrumb("Test subpage")]
    public IActionResult Sub()
    {
        _breadcrumbService.Add(new BreadcrumbItem()
        {
            Action = "Index",
            Controller = "Test",
            Area = "AreaTest",
            Parameters = new { Id = 12 },
            Title = "Insert via service"
        });        
        
        _breadcrumbService.Add(new BreadcrumbItem()
        {
            Action = "NotFound",
            Controller = "Test",
            Area = "AreaTest",
            Parameters = new { Id = 12 },
            Title = "Insert via service second"
        });
        
        return View("Index");
    }
}