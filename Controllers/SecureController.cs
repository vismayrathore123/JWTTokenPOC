using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class SecureController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
    