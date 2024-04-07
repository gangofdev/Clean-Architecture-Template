using CleanArc.Domain.Contracts;
using CleanArc.Domain.Contracts.Identity;
using CleanArc.Domain.Contracts.Persistence;
using Mediator;

namespace CleanArc.Application.Features.Order.Commands;

internal class AddOrderCommandHandler:IRequestHandler<AddOrderCommand,OperationResult<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserManager _userManager;
    private readonly IOrderContract _orderContract;

    public AddOrderCommandHandler(IUnitOfWork unitOfWork, IAppUserManager userManager,IOrderContract orderContract)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _orderContract = orderContract;
    }

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