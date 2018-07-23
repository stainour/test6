using Kendo.Mvc.UI;
using MediatR;

namespace WebApp.Queries
{
    internal class GetAuthorsQuery : IRequest<DataSourceResult>
    {
        public GetAuthorsQuery(DataSourceRequest request)
        {
            Request = request;
        }

        public DataSourceRequest Request { get; }
    }
}