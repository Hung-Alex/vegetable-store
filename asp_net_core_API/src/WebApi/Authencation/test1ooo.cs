using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace WebApi.Authencation
{
    public class test1ooo : AuthenticationHandler<test1>
    {
        public test1ooo(
           IOptionsMonitor<test1> options,
           ILoggerFactory logger,
           UrlEncoder encoder,
           ISystemClock clock) : base(options, logger, encoder, clock)
        {


        }
        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {

            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Not authorized");
            }
            var principal = new ClaimsPrincipal(new ClaimsIdentity("Test"));
            var ticket = new AuthenticationTicket(principal, this.Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}
