using System.Net;
using System.Net.Mail;

using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.UserService.Configurations;
using Bank.UserService.Models;
using Bank.UserService.Repositories;

namespace Bank.UserService.Services;

public interface IEmailService
{
    public Task<Result> Send(EmailType type, User user);

    public Task<Result> Send(EmailType type, User user, params object[] formatArgs);
}

public class EmailService(IEmailRepository emailRepository, IAuthorizationService authorizationService) : IEmailService
{
    private readonly IEmailRepository      m_EmailRepository      = emailRepository;
    private readonly IAuthorizationService m_AuthorizationService = authorizationService;

    public async Task<Result> Send(EmailType type, User user)
    {
        var email = m_EmailRepository.Find(type);

        try
        {
            var client = new SmtpClient(Configuration.Email.Server, Configuration.Email.Port);
            client.EnableSsl             = true;
            client.UseDefaultCredentials = false;
            client.Credentials           = new NetworkCredential(Configuration.Email.Address, Configuration.Email.Password);

            var body = type == EmailType.UserActivateAccount
                       ? email.FormatBody($"{Configuration.Frontend.BaseUrl}{Configuration.Frontend.Route.Activate}?token={m_AuthorizationService.GenerateTokenFor(user)}")
                       : email.FormatBody($"{Configuration.Frontend.BaseUrl}{Configuration.Frontend.Route.ResetPassword}?token={m_AuthorizationService.GenerateTokenFor(user)}");

            var mailMessage = new MailMessage();
            mailMessage.From       = new MailAddress(Configuration.Email.Address);
            mailMessage.Subject    = email.Subject;
            mailMessage.Body       = body;
            mailMessage.IsBodyHtml = true;
            mailMessage.To.Add(user.Email);

            if (Configuration.Application.Profile == Profile.Testing)
                return Result.Ok();

            await client.SendMailAsync(mailMessage);

            return Result.Ok();
        }
        catch (Exception _)
        {
            return Result.BadRequest();
        }
    }

    public async Task<Result> Send(EmailType type, User user, params object[] formatArgs)
    {
        var email = m_EmailRepository.Find(type);

        try
        {
            var client = new SmtpClient(Configuration.Email.Server, Configuration.Email.Port);
            client.EnableSsl             = true;
            client.UseDefaultCredentials = false;
            client.Credentials           = new NetworkCredential(Configuration.Email.Address, Configuration.Email.Password);

            // Zameni dvostruke zagrade {{X}} sa format-friendly {X}
            var formattedBody = email.Body.Replace("{{0}}", "{0}")
                                     .Replace("{{1}}", "{1}")
                                     .Replace("{{2}}", "{2}")
                                     .Replace("{{3}}", "{3}");

            var body = string.Format(formattedBody, formatArgs);

            var mailMessage = new MailMessage
                              {
                                  From       = new MailAddress(Configuration.Email.Address),
                                  Subject    = email.Subject,
                                  Body       = body,
                                  IsBodyHtml = true
                              };

            mailMessage.To.Add(user.Email);

            if (Configuration.Application.Profile == Profile.Testing)
                return Result.Ok();

            await client.SendMailAsync(mailMessage);
            return Result.Ok();
        }
        catch (Exception _)
        {
            return Result.BadRequest();
        }
    }
}
