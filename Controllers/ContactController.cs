using Microsoft.AspNetCore.Mvc;
using PortfolioWebsite.Models;
using PortfolioWebsite.Services.Interfaces;

namespace PortfolioWebsite.Controllers
{
	public class ContactController : Controller
	{
		private readonly IEmailService _emailService;
		public ContactController( IEmailService emailService)
		{
			_emailService = emailService;
		}
		public IActionResult Contact()
		{
			return View(new ContactViewModel());
		}

		[HttpPost]
		public async Task<IActionResult> Contact(ContactViewModel model)
		{
			if (!ModelState.IsValid)
			{
				TempData["Error"] = "Please fill all required fields.";
				return RedirectToAction("Index");
			}

			try
			{
				await _emailService.SendEmailAsync(model);

				TempData["Success"] = "Your message has been sent successfully!";
			}
			catch
			{
				TempData["Error"] = "Something went wrong while sending the email.";
			}

			return RedirectToAction("Index");
		}

	}
}
