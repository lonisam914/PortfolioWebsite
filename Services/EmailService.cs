// PortfolioWebsite.Services
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using PortfolioWebsite.Models;
using PortfolioWebsite.Services.Interfaces;
using PortfolioWebsite.Models;
using PortfolioWebsite.Services.Interfaces;
using System.Net.Mail;

namespace PortfolioWebsite.Services
{
	public class EmailService : IEmailService
	{
		private readonly EmailSettings _settings;

		public EmailService(IOptions<EmailSettings> options)
		{
			_settings = options.Value;
		}

		public async Task SendEmailAsync(ContactViewModel model)
		{
			var message = new MimeMessage();

			message.From.Add(
				new MailboxAddress("Portfolio", _settings.FromEmail));

			message.To.Add(
				MailboxAddress.Parse(_settings.ToEmail));

			message.Subject = model.Subject;

			message.Body = new TextPart("plain")
			{
				Text =
$@"Name: {model.Name}

Email: {model.Email}

Message:

{model.Message}"
			};

			using var smtp = new MailKit.Net.Smtp.SmtpClient();

			await smtp.ConnectAsync(
				_settings.Host,
				_settings.Port,
				MailKit.Security.SecureSocketOptions.StartTls);

			await smtp.AuthenticateAsync(
				_settings.FromEmail,
				_settings.Password);

			await smtp.SendAsync(message);

			await smtp.DisconnectAsync(true);
		}
	}
}
