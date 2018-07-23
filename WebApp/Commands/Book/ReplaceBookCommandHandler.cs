using AutoMapper;
using Domain;
using MediatR;
using MemoryStorage;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Commands.Book
{
    public class ReplaceBookCommandHandler : AsyncRequestHandler<ReplaceBookCommand>
    {
        private readonly BookEditorContext _context;
        private readonly IMapper _mapper;

        public ReplaceBookCommandHandler(BookEditorContext context)
        {
            _context = context;
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Domain.Book, IdentityBookDto>().ReverseMap();
                cfg.CreateMap<Domain.Author, IdentityAuthorDto>().ReverseMap();
            }).CreateMapper();
        }

        protected override async Task Handle(ReplaceBookCommand command, CancellationToken cancellationToken)
        {
            var book = _mapper.Map<IdentityBookDto, Domain.Book>(command.BookDto);

            _context.AuthorBooks.RemoveRange(_context.AuthorBooks.Where(x => x.BookId == book.Id));
            _context.AuthorBooks.AddRange(command.BookDto.Authors.Select(dto => new AuthorBook(book.Id, dto.Id)));
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
    }
}