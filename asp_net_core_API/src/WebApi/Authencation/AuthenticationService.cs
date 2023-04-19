using Azure.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using store_vegetable.Services.StoreVegetable;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Channels;
using WebApi.JwtToken;

namespace WebApi.Authencation
{
    public class AuthenticationService : AuthenticationHandler<TokenAuthenticationSchemeOptions>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserTokenRepository _userTokenRepository;
        private readonly IJwtTokenRepository _jwtTokenRepository ;


        public AuthenticationService(
            IOptionsMonitor<TokenAuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            IUserTokenRepository userToken,
            IJwtTokenRepository jwtTokenRepository,
            IUserRepository user,
            ISystemClock clock) : base(options, logger, encoder, clock)
        {
           _userRepository= user;
            _userTokenRepository= userToken;
            _jwtTokenRepository = jwtTokenRepository;

        }
        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {

            /// kiem token co duoc gui hay ko
            /// kiem tra token co hop le hay ko
            /// kiem tra token trong usertoken co hay khong hay null
            /// neu hop le het thif chuyen nos qua middware
            /// 
            
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Not authorized");
            }

            var token = Request.Headers["Authorization"];
            if (!await _jwtTokenRepository.IsJwtTokenValid(token))
            {
                return AuthenticateResult.Fail("Not authorized");
            }

            var id =  await _jwtTokenRepository.GetInfoFromToken(token);
            var user =await _userRepository.GetById(id);
            if (user==null)
            {
                return AuthenticateResult.Fail("Not authorized");

            }
            if (!(user.Role=="admin"))
            {
                return AuthenticateResult.Fail("Not authorized");
            }
            if ( !await _userTokenRepository.CheckTokenIsExisted(user.Id,token))
            {
                return AuthenticateResult.Fail("Not authorized");
            }
            if (!user.UserToken.Status)
            {
                return AuthenticateResult.Fail("Not authorized");
            }

            var identity = new ClaimsIdentity("Test");
            identity.AddClaim(new Claim("Id", user.Id.ToString()));
            identity.AddClaim(new Claim("Role", user.Role));
            var principal = new ClaimsPrincipal(identity);
            

            var ticket = new AuthenticationTicket(principal, "Admin");
            return AuthenticateResult.Success(ticket);
        }
    }
}
