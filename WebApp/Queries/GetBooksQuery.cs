using Kendo.Mvc.UI;
using MediatR;

namespace WebApp.Queries
{
    internal class GetBooksQuery : IRequest<DataSourceResult>
    {
        public GetBooksQuery(DataSourceRequest request)
        {
            Request = request;
        }

        public DataSourceRequest Request { get; }
    }
}