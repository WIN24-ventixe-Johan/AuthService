using SendGrid;
using SendGrid.Helpers.Mail;

namespace JWT.Services;

public class EmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        var apiKey = _config["SendGrid:ApiKey"];
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("johanstroberg91@gmail.com", "Ventixe");
        var to = new EmailAddress(toEmail);
        var msg = MailHelper.CreateSingleEmail(from,to,subject,message,message);
        var response = await client.SendEmailAsync(msg);

        if(!response.IsSuccessStatusCode)
        {
            throw new Exception($"SendGrid Failed: {response.StatusCode}");
        }
    }
}
