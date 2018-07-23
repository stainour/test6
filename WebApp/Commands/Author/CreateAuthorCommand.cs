using MediatR;
using System;
using WebApp.Models;

namespace WebApp
{
    public class CreateAuthorCommand : IRequest
    {
        public CreateAuthorCommand(AuthorDto authorDto) => Dto = authorDto ?? throw new ArgumentNullException(nameof(authorDto));

        public AuthorDto Dto { get; }
    }
}