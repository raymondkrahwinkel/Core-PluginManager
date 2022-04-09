using System.Diagnostics;
using CorePluginManager.Alert.Services;
using CorePluginManager.Breadcrumb.Attributes;
using Microsoft.AspNetCore.Mvc;
using Demo.Models;

namespace Demo.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IAlertService _alertService;

    public HomeController(ILogger<HomeController> logger, IAlertService alertService)
    {
        _logger = logger;
        _alertService = alertService;
    }

    //[Breadcrumb("Homepage")]
    [DefaultBreadcrumb("Home")]
    public async Task<IActionResult> Index()
    {
        _alertService.Error("Test!");
        return View();
    }

    [Breadcrumb("Privacy")]
    public async Task<IActionResult> Privacy()
    {
        var test = _alertService.GetErrorMessages();
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}