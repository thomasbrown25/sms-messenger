using sms_messenger_api.Models;

namespace sms_messenger_api.Services;

// Stub implementation — replace with a real provider (Twilio, AWS SNS, etc.)
// For Twilio: install Twilio NuGet package and implement using TwilioClient
public class SmsService : ISmsService
{
    private readonly IConfiguration _config;
    private readonly ILogger<SmsService> _logger;

    public SmsService(IConfiguration config, ILogger<SmsService> logger)
    {
        _config = config;
        _logger = logger;
    }

    public Task<SmsResponse> SendAsync(SmsRequest request)
    {
        // TODO: wire up your SMS provider here
        // Example Twilio implementation:
        //   TwilioClient.Init(_config["Sms:AccountSid"], _config["Sms:AuthToken"]);
        //   var message = await MessageResource.CreateAsync(
        //       body: request.Message,
        //       from: new PhoneNumber(_config["Sms:FromNumber"]),
        //       to: new PhoneNumber(request.ToPhoneNumber));
        //   return new SmsResponse { Success = true, MessageSid = message.Sid };

        _logger.LogInformation("SMS stub: sending to {To}: {Message}", request.ToPhoneNumber, request.Message);

        return Task.FromResult(new SmsResponse
        {
            Success = true,
            MessageSid = $"STUB-{Guid.NewGuid():N}"
        });
    }
}
