using MediatR;
using System;
using WebApp.Models;

namespace WebApp.Commands.Author
{
    public class ReplaceAuthorCommand : IRequest
    {
        public ReplaceAuthorCommand(IdentityAuthorDto dto) => Dto = dto ?? throw new ArgumentNullException(nameof(dto));

        public IdentityAuthorDto Dto { get; }
    }
}