using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;
using MailKit.Security;

public class EmailSettings
{
    public string SmtpServer { get; set; }
    public int SmtpPort { get; set; }
    public string SenderEmail { get; set; }
    public string SenderPassword { get; set; }
    public string RecipientEmail { get; set; }
}

public class EmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendEmailAsync(string subject, string body)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("Your Application", _emailSettings.SenderEmail));
        emailMessage.To.Add(new MailboxAddress("", _emailSettings.RecipientEmail));
        emailMessage.Subject = subject;

        var bodyBuilder = new BodyBuilder
        {
            TextBody = body
        };

        emailMessage.Body = bodyBuilder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.SenderPassword);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}
