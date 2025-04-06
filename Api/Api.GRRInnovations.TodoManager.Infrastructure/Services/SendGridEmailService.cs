using Api.GRRInnovations.TodoManager.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SendGrid;
using Microsoft.Extensions.Options;
using Api.GRRInnovations.TodoManager.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Services
{
    public class SendGridEmailService : IEmailService
    {
        private readonly SendGridSettings _settings;
        private readonly ILogger<SendGridEmailService> _logger;

        public SendGridEmailService(IOptions<SendGridSettings> options, ILogger<SendGridEmailService> logger)
        {
            _settings = options.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            try
            {
                var client = new SendGridClient(_settings.ApiKey);
                var from = new EmailAddress(_settings.FromEmail, _settings.FromEmail);
                var to = new EmailAddress(toEmail, _settings.FromEmail);
                var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";

                var msg = MailHelper.CreateSingleEmail(from, to, subject, message, htmlContent);
                var request = await client.SendEmailAsync(msg).ConfigureAwait(false);

                if (request.IsSuccessStatusCode == false)
                {
                    var body = await request.Body.ReadAsStringAsync();
                    throw new Exception($"Failed to send email: {body}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error to send email: {error}", ex.Message);
            }
        }
    }
}
