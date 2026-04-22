using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using sms_messenger_api.Models;

namespace sms_messenger_api.Services;

public class SmsService : ISmsService
{
    private readonly TwilioSettings _settings;
    private readonly ILogger<SmsService> _logger;

    public SmsService(IOptions<TwilioSettings> settings, ILogger<SmsService> logger)
    {
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task<SmsResponse> SendAsync(SmsRequest request)
    {
        TwilioClient.Init(_settings.AccountSid, _settings.AuthToken);

        var message = await MessageResource.CreateAsync(
            body: request.Message,
            from: new PhoneNumber(_settings.FromNumber),
            to: new PhoneNumber(request.ToPhoneNumber));

        _logger.LogInformation("SMS sent to {To} — SID: {Sid} Status: {Status}",
            request.ToPhoneNumber, message.Sid, message.Status);

        return new SmsResponse
        {
            Success = true,
            MessageSid = message.Sid
        };
    }
}
