using Microsoft.Extensions.Options;
using store_vegetable.Core.DTO;
using WebApi.Models;
using WebApi.Settings;

namespace WebApi.Mail
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
