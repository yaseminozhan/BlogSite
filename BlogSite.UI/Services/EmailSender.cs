using Microsoft.AspNetCore.Identity.UI.Services;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BlogSite.UI.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            Debug.WriteLine($" GÖNDERİLEN EMAIL -> {email}");
            Debug.WriteLine($" KONU -> {subject}");
            Debug.WriteLine($" İÇERİK -> {htmlMessage}");

         
            return Task.CompletedTask;
        }
    }
}
