using Microsoft.AspNetCore.Mvc;

namespace QueueProgram.Controllers;

public class PagesController : Controller
{
    public IActionResult Index()
    {
        return View("~/Views/Home/Index.cshtml");
    }
    
    public IActionResult LiveQueue()
    {
        return View("~/Views/LiveQueue/LiveQueue.cshtml");
    }
    
    public IActionResult Admin()
    {
        return View("~/Views/Admin/Admin.cshtml");
    }
}