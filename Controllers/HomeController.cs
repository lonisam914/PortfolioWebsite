using Microsoft.AspNetCore.Mvc;
using PortfolioWebsite.Models;
using PortfolioWebsite.Services.Interfaces;
using System.Diagnostics;

namespace PortfolioWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly IEmailService _emailService;
		public HomeController(ILogger<HomeController> logger, IEmailService emailService)
        {
            _logger = logger;
			_emailService = emailService;
		}

        public IActionResult Index()
        {
			return View(new ContactViewModel());
		}
	
		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
