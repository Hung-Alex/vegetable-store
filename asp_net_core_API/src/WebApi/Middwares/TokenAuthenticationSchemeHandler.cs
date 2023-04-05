using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace WebApi.Middwares
{
    public class TokenAuthenticationSchemeHandler : AuthenticationHandler<TokenAuthenticationSchemeOptions>
    {
        public TokenAuthenticationSchemeHandler(
            IOptionsMonitor<TokenAuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Read the token from request headers/cookies
            // Check that it's a valid session, depending on your implementation

            // If the session is valid, return success:
            var principal = new ClaimsPrincipal(new ClaimsIdentity("Test"));
            var ticket = new AuthenticationTicket(principal, this.Scheme.Name);
            return AuthenticateResult.Success(ticket);

            // If the token is missing or the session is invalid, return failure:
            //return AuthenticateResult.Fail("Authentication failed");
        }
    }

}
