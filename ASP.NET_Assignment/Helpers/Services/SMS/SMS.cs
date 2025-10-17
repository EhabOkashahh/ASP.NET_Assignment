using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace ASP.NET.Assignment.PL.Helpers.Services.SMS
{
    public class SMS(IOptions<TwilioSettings> _options) : ISMS
    {

        public bool SendSMS(SMSStructure sMSStructure)
        {
            try
            {
                var sid = _options.Value.AccountSID.Trim('"');
                var token = _options.Value.AuthToken.Trim('"');
                //Establish Connection
                TwilioClient.Init(sid, token);

                //Build Message
                var message = MessageResource.Create(
                to: sMSStructure.To,
                body: sMSStructure.Body,
                from: _options.Value.PhoneNumber
                );

                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }
    }
}
