using CleanArc.Domain.Contracts;
using CleanArc.Domain.Contracts.Identity;
using CleanArc.Domain.Contracts.Persistence;
using Mediator;

namespace CleanArc.Application.Features.Order.Commands;

internal class AddOrderCommandHandler(IUnitOfWork unitOfWork, IAppUserManager userManager, IOrderContract orderContract) : IRequestHandler<AddOrderCommand,OperationResult<bool>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAppUserManager _userManager = userManager;
    private readonly IOrderContract _orderContract = orderContract;

    public async ValueTask<OperationResult<bool>> Handle(AddOrderCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.GetUserByIdAsync(request.UserId);

        if(user==null)
            return OperationResult<bool>.FailureResult("User Not Found");

        //await _unitOfWork.OrderRepository.AddOrderAsync(new Domain.Entities.Order.Order()
        //    { UserId = user.Id, OrderName = request.OrderName });

        //await _unitOfWork.CommitAsync();
        await this._orderContract.PlaceOrder(new Domain.Entities.Order.Order()
            { UserId = user.Id, OrderName = request.OrderName });

        return OperationResult<bool>.SuccessResult(true);
    }
}