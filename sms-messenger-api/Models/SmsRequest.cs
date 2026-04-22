using System.ComponentModel.DataAnnotations;

namespace sms_messenger_api.Models;

public class SmsRequest
{
    [Required]
    [RegularExpression(@"^\+1\d{10}$", ErrorMessage = "Phone number must be in E.164 format: +1XXXXXXXXXX")]
    public string ToPhoneNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(1600, MinimumLength = 1, ErrorMessage = "Message must be between 1 and 1600 characters")]
    public string Message { get; set; } = string.Empty;
}
