using AutoMapper;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using MediatR;
using MemoryStorage;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Queries
{
    internal class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, DataSourceResult>
    {
        private readonly BookEditorContext _context;
        private readonly IMapper _mapper;

        public GetBooksQueryHandler(BookEditorContext context)
        {
            _context = context;

            _mapper = new MapperConfiguration(cfg =>
                     {
                         cfg.CreateMap<Domain.Author, IdentityAuthorDto>();
                         cfg.CreateMap<Domain.Book, IdentityBookDto>();
                     }).CreateMapper();
        }

        public async Task<DataSourceResult> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var dataSourceResult = await _context.Books.Include(b => b.AuthorBooks).ThenInclude(b => b.Author).ToDataSourceResultAsync(request.Request);

            dataSourceResult.Data = dataSourceResult.Data.Cast<Domain.Book>().Select(book =>
            {
                var bookDto = _mapper.Map<Domain.Book, IdentityBookDto>(book);
                bookDto.Authors = _mapper.Map<IEnumerable<Domain.Author>, IEnumerable<IdentityAuthorDto>>(book.AuthorBooks.Select(authorBook => authorBook.Author)).ToList();
                return bookDto;
            }).ToList();

            return dataSourceResult;
        }
    }
}