using MediatR;
using System;
using WebApp.Models;

namespace WebApp.Commands.Book
{
    public class ReplaceBookCommand : IRequest
    {
        public ReplaceBookCommand(IdentityBookDto bookDto) => BookDto = bookDto ?? throw new ArgumentNullException(nameof(bookDto));

        public IdentityBookDto BookDto { get; }
    }
}