using MediatR;
using MemoryStorage;
using System.Threading;
using System.Threading.Tasks;

namespace WebApp.Commands.Author
{
    public class CreateAuthorCommandHandler : AsyncRequestHandler<CreateAuthorCommand>
    {
        private readonly BookEditorContext _context;

        public CreateAuthorCommandHandler(BookEditorContext context)
        {
            _context = context;
        }

        protected override async Task Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var authorDto = request.Dto;
            _context.Authors.Add(new Domain.Author(authorDto.FirstName, authorDto.LastName));
            await _context.SaveChangesAsync();
        }
    }
}