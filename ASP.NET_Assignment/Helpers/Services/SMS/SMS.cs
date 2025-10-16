using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace ASP.NET.Assignment.PL.Helpers.Services.SMS
{
    public class SMS(IOptions<SMSSettings> _options) : ISMS
    {
        public MessageResource SendSMS(SMSStructure sMSStructure)
        {
            //Establish Connection
            TwilioClient.Init(_options.Value.AccountSID,_options.Value.AuthToken);

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
