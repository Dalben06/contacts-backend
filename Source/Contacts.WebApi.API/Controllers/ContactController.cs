using Contacts.Auth.Application.Services;
using Contacts.Contact.Application.Services;
using Contacts.Contact.Application.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.WebApi.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ApiController
    {
        private readonly IPersonAppService _personService;

        public ContactController(IPersonAppService personService, IAuthService authService): base(authService)
        {
            this._personService = personService;
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return HttpResponse(await this._personService.GetContacts(this.userSession.UserId));
        }

        [HttpGet("GetFilter/{filter}")]
        public async Task<IActionResult> GetAll(string filter)
        {
            return HttpResponse(await this._personService.GetContactsFromFilter(filter, this.userSession.UserId));
        }

        [HttpGet("Get/{Id}")]
        public async Task<IActionResult> Get(Guid Id)
        {
            return HttpResponse(await this._personService.GetContact(Id, this.userSession.UserId));
        }

        [HttpPost("CreateContact")]
        public async Task<IActionResult> CreateClub(PersonViewModel model)
        {
            model.UserId = this.userSession.UserId;
            return HttpResponse(await this._personService.CreateContact(model));
        }

        [HttpPut("UpdateContact")]
        public async Task<IActionResult> UpdateContact(PersonViewModel model)
        {
            model.UserId = this.userSession.UserId;
            return HttpResponse(await this._personService.UpdateContact(model));
        }


        [HttpDelete("DisableContact/{Id}")]
        public async Task<IActionResult> DisableClub(Guid Id)
        {
            return HttpResponse(await this._personService.RemoveContact(Id, this.userSession.UserId));
        }
    }
}
