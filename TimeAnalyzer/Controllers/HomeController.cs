using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeAnalyzer.Domain.Interfaces;

namespace TimeAnalyzer.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }
        
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
