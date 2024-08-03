using SendGrid.Helpers.Mail;
using SendGrid;



namespace MangoShop.Api.Helper;



/// <summary>
/// Class for sending Emails
/// </summary>
public class EmailService
{


    private readonly string apiKey;


    public EmailService(string sendGridApiKey)
    {
        apiKey = sendGridApiKey;
    }



    public async Task<bool> SendEmailAsync(string toEmail, string subject, string plainTextContent, string htmlContent)
    {

        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("test@gmail.com", "Test User");
        var to = new EmailAddress(toEmail);
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

        var response = await client.SendEmailAsync(msg);
        Console.WriteLine(response.StatusCode);

        if (response.StatusCode != System.Net.HttpStatusCode.OK)
        {
            var errorMessage = await response.Body.ReadAsStringAsync();
            Console.WriteLine($"Error sending email: {errorMessage}");
        }

        return response.StatusCode == System.Net.HttpStatusCode.OK;

    }

}

