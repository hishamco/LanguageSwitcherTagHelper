using Microsoft.AspNetCore.Mvc;

namespace LanguageSwitcherTagHelper
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index() => View();
    }
}