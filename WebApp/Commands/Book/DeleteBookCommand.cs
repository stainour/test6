using MediatR;
using System;

namespace WebApp.Commands.Book
{
    public class DeleteBookCommand : IRequest<DeleteResult>
    {
        public DeleteBookCommand(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            Id = id;
        }

        public int Id { get; }
    }
}