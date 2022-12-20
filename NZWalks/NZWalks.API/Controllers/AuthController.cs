using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository iuserrepository;
        private readonly ITokenHandler tokenhandler;
        public AuthController(IUserRepository obj,ITokenHandler objtokenHandler)
        {
            this.iuserrepository = obj;
            this.tokenhandler = objtokenHandler;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(DTO.LoginRequest loginRequest)
        {
            //check if user is authenticated
            //check user and password
            var user = await iuserrepository.AuthenticateAsync
                (loginRequest.UserName,loginRequest.Password);

            if (user!=null)
            {
                //generate JWT
                var token=await tokenhandler.CreateTokenAsync(user);
                return Ok(token);
            }
            return BadRequest("username and password is incorrect");
        }
    }
}
