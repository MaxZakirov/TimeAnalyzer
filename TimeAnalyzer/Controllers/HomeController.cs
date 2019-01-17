using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TimeAnalyzer.Domain.Interfaces;

namespace TimeAnalyzer.Controllers
{
    public class HomeController : Controller
    {
        public IStringLocalizer<HomeController> _Localizer { get; }

        public HomeController(IStringLocalizer<HomeController> _localizer)
        {
            _Localizer = _localizer;
        }
        
        public IActionResult Index()
        {
            var s = _Localizer["Test"];
            ViewData["loc"] = _Localizer.GetAllStrings();
            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }

    public static class LocaleStatus
    {
        public static bool IsEnglish { get; set; }

        public static CultureInfo GetCulture()
        {
            if(IsEnglish)
            {
                return new CultureInfo("en");
            }
            else
            {
                return new CultureInfo("ru");
            }
        }
    }
}
