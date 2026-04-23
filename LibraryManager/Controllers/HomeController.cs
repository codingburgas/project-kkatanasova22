using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Ако потребителят е логнат, можеш да го препратиш директно към каталога
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Books");
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}