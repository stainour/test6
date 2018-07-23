using MediatR;
using MemoryStorage;
using System.Threading;
using System.Threading.Tasks;

namespace WebApp.Commands.Author
{
    public class ReplaceAuthorCommandHandler : AsyncRequestHandler<ReplaceAuthorCommand>
    {
        private readonly BookEditorContext _context;

        public ReplaceAuthorCommandHandler(BookEditorContext context)
        {
            _context = context;
        }

        protected override async Task Handle(ReplaceAuthorCommand command, CancellationToken cancellationToken)
        {
            var authorDto = command.Dto;
            var author = await _context.Authors.FindAsync(authorDto.Id);

            if (author != default)
            {
                author.UpdateNames(authorDto.FirstName, authorDto.LastName);
                _context.Authors.Update(author);
                await _context.SaveChangesAsync();
            }
        }
    }
}