using Microsoft.AspNetCore.Mvc;

namespace SmokingCessationSupportPlatform.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
} 