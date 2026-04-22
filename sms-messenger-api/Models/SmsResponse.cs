namespace sms_messenger_api.Models;

public class SmsResponse
{
    public bool Success { get; set; }
    public string? MessageSid { get; set; }
    public string? Error { get; set; }
}
