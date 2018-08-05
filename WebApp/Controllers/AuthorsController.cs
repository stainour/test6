using Kendo.Mvc.UI;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApp.Commands.Author;
using WebApp.Models;
using WebApp.Queries;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : Controller
    {
        private readonly IMediator _mediator;

        public AuthorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ModelStateValidation]
        public async Task<ActionResult> Create(AuthorDto dto)
        {
            await _mediator.Send(new CreateAuthorCommand(dto));
            return NoContent();
        }

        [HttpGet]
        public async Task<JsonResult> GetAuthors([DataSourceRequest]DataSourceRequest request)
        {
            return Json(await _mediator.Send(new GetAuthorsQuery(request)));
        }

        [HttpPut]
        [ModelStateValidation]
        public async Task<ActionResult> Replace(IdentityAuthorDto dto)
        {
            await _mediator.Send(new ReplaceAuthorCommand(dto));
            return NoContent();
        }
    }
}