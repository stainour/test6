using MediatR;
using System;

namespace WebApp.Commands.Author
{
    public class DeleteAuthorCommand : IRequest<DeleteResult>
    {
        public DeleteAuthorCommand(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            Id = id;
        }

        public int Id { get; }
    }
}