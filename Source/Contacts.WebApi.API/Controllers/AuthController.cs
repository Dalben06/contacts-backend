using Contacts.Auth.Application.Services;
using Contacts.Auth.Application.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.WebApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ApiController
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService): base(authService) 
        {
            this._authService = authService;
        }


        [HttpPost("Authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate(AuthViewModel model)
        {
            return HttpResponse(await this._authService.Login(model));
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(AuthViewModel authViewModel)
        {
            return HttpResponse(await this._authService.Register(authViewModel));
        }
    }
}
