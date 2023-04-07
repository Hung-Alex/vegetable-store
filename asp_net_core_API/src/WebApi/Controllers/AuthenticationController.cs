using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.IdentityModel.Tokens;
using store_vegetable.Core.Entites;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Models;

namespace WebApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
       
        [HttpPost("V1/Login")]
        //[Authorize(AuthenticationSchemes = "Tokens")]
        public async Task<IActionResult> Login(LoginModel login)
        {
            return StatusCode(StatusCodes.Status302Found);
        }
     
        

      
    }
}
