using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Twilio.Rest.Api.V2010.Account;

namespace ASP.NET.Assignment.PL.Helpers.Services.SMS
{
    public interface ISMS
    {
        public bool SendSMS(SMSStructure sMSStructure);
    }
}
