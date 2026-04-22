using Microsoft.AspNetCore.Mvc;
using sms_messenger_api.Models;
using sms_messenger_api.Services;

namespace sms_messenger_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SmsController : ControllerBase
{
    private readonly ISmsService _smsService;

    public SmsController(ISmsService smsService)
    {
        _smsService = smsService;
    }

    [HttpPost("send")]
    public async Task<ActionResult<SmsResponse>> Send([FromBody] SmsRequest request)
    {
        var result = await _smsService.SendAsync(request);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }
}
