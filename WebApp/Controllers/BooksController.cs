using Kendo.Mvc.UI;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebApp.Commands;
using WebApp.Commands.Book;
using WebApp.Models;
using WebApp.Queries;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : Controller
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ModelStateValidation]
        public async Task<ActionResult> Create(BookDto dto)
        {
            await _mediator.Send(new CreateBookCommand(dto));
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([Range(1, int.MaxValue)]int id)
        {
            var deleteResult = await _mediator.Send(new DeleteBookCommand(id));

            if (deleteResult == DeleteResult.AlreadyDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet]
        public async Task<JsonResult> GetBooks([DataSourceRequest]DataSourceRequest request)
        {
            return Json(await _mediator.Send(new GetBooksQuery(request)));
        }

        [HttpPut]
        [ModelStateValidation]
        public async Task<ActionResult> Replace(IdentityBookDto dto)
        {
            await _mediator.Send(new ReplaceBookCommand(dto));
            return NoContent();
        }
    }
}