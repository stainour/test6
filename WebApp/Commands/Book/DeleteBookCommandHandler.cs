using MediatR;
using MemoryStorage;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace WebApp.Commands.Book
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, DeleteResult>
    {
        private readonly BookEditorContext _context;

        public DeleteBookCommandHandler(BookEditorContext context)
        {
            _context = context;
        }

        public async Task<DeleteResult> Handle(DeleteBookCommand command, CancellationToken cancellationToken)
        {
            var author = await _context.Books.FindAsync(command.Id);

            if (author != default)
            {
                _context.Books.Remove(author);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return DeleteResult.AlreadyDeleted;
                }
                return DeleteResult.OK;
            }
            return DeleteResult.AlreadyDeleted;
        }
    }
}