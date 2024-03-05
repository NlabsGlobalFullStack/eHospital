using Business.Constans;
using GenericEmailService;

namespace Business.Services;
public static class EmailHelper
{
    public static async Task<string> SendEmailAsync(string email, string subject, string body)
    {
        EmailConfigurations emailConfigurations = new(
                EmailConstants.Smtp,
                EmailConstants.Password,
                EmailConstants.Port,
                EmailConstants.SSL,
                EmailConstants.Html);

        EmailModel<Stream> emailModel = new(
            emailConfigurations,
            "iyzico1@outlook.com",
            new List<string> { email },
            subject,
            body,
            null);

        string emailResponse = await EmailService.SendEmailWithMailKitAsync(emailModel);
        return emailResponse;
    }
}
