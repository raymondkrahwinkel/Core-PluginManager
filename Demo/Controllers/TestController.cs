using CorePluginManager.Plugins.Breadcrumb.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers;

[Breadcrumb("Test class")]
[Area("AreaName")]
public class TestController : Controller
{
    [Breadcrumb("Test page")]
    public IActionResult Index()
    {
        return View();
    }
}