using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Aerith.Api.Helpers
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.CompletedTask;
        }
    }
}