using PortfolioWebsite.Models;

namespace PortfolioWebsite.Services.Interfaces
{
	public interface IEmailService
	{
		Task SendEmailAsync(ContactViewModel model);
	}
}
