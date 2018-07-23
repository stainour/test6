using MediatR;
using WebApp.Models;

namespace WebApp.Commands.Book
{
    internal class CreateBookCommand : IRequest
    {
        public CreateBookCommand(BookDto bookDto)
        {
            BookDto = bookDto;
        }

        public BookDto BookDto { get; }
    }
}