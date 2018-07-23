using AutoMapper;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using MediatR;
using MemoryStorage;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Queries
{
    internal class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, DataSourceResult>
    {
        private readonly BookEditorContext _context;
        private readonly IMapper _mapper;

        public GetAuthorsQueryHandler(BookEditorContext context)
        {
            _context = context;
            _mapper = new MapperConfiguration(cfg => cfg.CreateMap<Domain.Author, IdentityAuthorDto>()).CreateMapper();
        }

        public async Task<DataSourceResult> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
        {
            var dataSourceResult = await _context.Authors.ToDataSourceResultAsync(request.Request);
            dataSourceResult.Data = dataSourceResult.Data.Cast<Domain.Author>().Select(author => _mapper.Map<Domain.Author, IdentityAuthorDto>(author)).ToList();
            return dataSourceResult;
        }
    }
}