using Microsoft.AspNetCore.Authentication;

namespace WebApi.Authencation
{
    public class TokenAuthenticationSchemeOptions: AuthenticationSchemeOptions
    {
        public string Role { get; set; }
    }
}
