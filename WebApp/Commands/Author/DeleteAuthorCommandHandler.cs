using MediatR;
using MemoryStorage;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace WebApp.Commands.Author
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, DeleteResult>
    {
        private readonly BookEditorContext _context;

        public DeleteAuthorCommandHandler(BookEditorContext context)
        {
            _context = context;
        }

        public async Task<DeleteResult> Handle(DeleteAuthorCommand command, CancellationToken cancellationToken)
        {
            var author = await _context.Authors.FindAsync(command.Id);

            if (author != default)
            {
                _context.Authors.Remove(author);
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