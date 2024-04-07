using CleanArc.Domain.Contracts;
using CleanArc.Domain.Contracts.Persistence;
using CleanArc.Domain.Models;

namespace CleanArc.Application.Features.Order.Queries.GetPagedOrders
{
    internal class GetPagedOrdersQueryHandler:IRequestHandler<GetPagedOrdersQuery, OperationResult<GetPagedOrdersQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        IOrderContract _orderContract;

        public GetPagedOrdersQueryHandler(IUnitOfWork unitOfWork, IOrderContract orderContract)
        {
            _unitOfWork = unitOfWork;
            _orderContract = orderContract;
        }

        public async ValueTask<OperationResult<GetPagedOrdersQueryResult>> Handle(GetPagedOrdersQuery request, CancellationToken cancellationToken)
        {
            var result = await this._orderContract.GetPagedOrders(new PagedRequest() { PageIndex = request.PageIndex, PageSize = request.PageSize });


            return OperationResult<GetPagedOrdersQueryResult>.SuccessResult(new GetPagedOrdersQueryResult(result.Result));
        }
    }
}
