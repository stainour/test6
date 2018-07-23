using AutoMapper;
using MediatR;
using MemoryStorage;
using System.Threading;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Commands.Book
{
    internal class CreateBookCommandHandler : AsyncRequestHandler<CreateBookCommand>
    {
        private readonly BookEditorContext _context;
        private readonly IMapper _mapper;

        public CreateBookCommandHandler(BookEditorContext context)
        {
            _context = context;
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Domain.Book, BookDto>().ReverseMap();
                cfg.CreateMap<Domain.Author, AuthorDto>().ReverseMap();
            }).CreateMapper();
        }

        protected override async Task Handle(CreateBookCommand command, CancellationToken cancellationToken)
        {
            var book = _mapper.Map<BookDto, Domain.Book>(command.BookDto);

            command.BookDto.Authors.ForEach(dto => book.AddAuthor(_mapper.Map<AuthorDto, Domain.Author>(dto)));

            _context.Books.Add(book);

            await _context.SaveChangesAsync();
        }
    }
}