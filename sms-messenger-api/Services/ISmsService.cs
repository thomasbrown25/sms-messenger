using sms_messenger_api.Models;

namespace sms_messenger_api.Services;

public interface ISmsService
{
    Task<SmsResponse> SendAsync(SmsRequest request);
}
