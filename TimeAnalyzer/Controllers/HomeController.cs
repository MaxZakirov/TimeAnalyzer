using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeAnalyzer.Domain.Interfaces;

namespace TimeAnalyzer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICredentialTypesRepository credentialTypesRepository;

        public HomeController(ICredentialTypesRepository credentialTypesRepository)
        {
            this.credentialTypesRepository = credentialTypesRepository;
        }

        public async Task<IActionResult> Index()
        {
            var c = await credentialTypesRepository.GetByCodeAsync("aaa");
            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
