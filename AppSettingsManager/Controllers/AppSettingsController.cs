using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AppSettingsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppSettingsController : ControllerBase
    {
        private readonly ILogger<AppSettingsController> _logger;
        private readonly IConfiguration _config;
        private TwilioSettings _settings;

        public AppSettingsController(ILogger<AppSettingsController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _settings = new TwilioSettings();
            config.GetSection("Twilio").Bind(_settings);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var sendGridKey = _config.GetValue<string>("SendGridKey");
            var twilioAuthToken = _config.GetSection("Twilio").GetValue<string>("AuthToken");
            //var twilioAccountSid = _config.GetValue<string>("Twilio:AccountSid");
            var twilioAccountSid = _config.GetSection("Twilio:AccountSid").Value;
            //var thirdLevelSetting = _config.GetSection("FirstLevelSetting").GetSection("SecondLevelSetting").GetValue<string>("BottomLevelSetting");
            var thirdLevelSetting = _config.GetSection("FirstLevelSetting").GetSection("SecondLevelSetting").GetSection("BottomLevelSetting").Value;
            var twilioPhoneNumber = _settings.PhoneNumber;
            return Ok(new
            {
                SendGridKey = sendGridKey,
                TwilioAuthToken = twilioAuthToken,
                TwilioAccountSid = twilioAccountSid,
                ThirdLevelSetting = thirdLevelSetting,
                TwilioPhoneNumber = twilioPhoneNumber
            }) ;
        }
    }
}
