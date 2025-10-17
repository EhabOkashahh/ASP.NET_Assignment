using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace ASP.NET.Assignment.PL.Helpers.Services.SMS
{
    public class SMS(IOptions<SMSSettings> _options) : ISMS
    {
        public MessageResource SendSMS(SMSStructure sMSStructure)
        {
            var sid = _options.Value.AccountSID.Trim('"');
            var token = _options.Value.AuthToken.Trim('"');
            //Establish Connection
            TwilioClient.Init(sid,token);

            //Build Message
            var message = MessageResource.Create(
                to: sMSStructure.To,
                body: sMSStructure.Body,
                from: _options.Value.PhoneNumber
            );
            return message;
        }
    }
}
