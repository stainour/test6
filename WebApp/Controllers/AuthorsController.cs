using Kendo.Mvc.UI;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebApp.Commands;
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

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([Range(1, int.MaxValue)]int id)
        {
            var deleteResult = await _mediator.Send(new DeleteAuthorCommand(id));

            if (deleteResult == DeleteResult.AlreadyDeleted)
            {
                return NotFound();
            }
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