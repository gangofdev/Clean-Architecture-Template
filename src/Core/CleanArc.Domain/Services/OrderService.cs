using Azure.Core;
using CleanArc.Domain.Contracts;
using CleanArc.Domain.Contracts.Identity;
using CleanArc.Domain.Contracts.Persistence;
using CleanArc.Domain.Entities.Order;
using CleanArc.Domain.Entities.User;
using CleanArc.Domain.Models;
using CleanArc.SharedKernel;
using CleanArc.SharedKernel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Services
{
    public class OrderService(IUnitOfWork unitOfWork, IAppUserManager userManager) : IOrderContract
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IAppUserManager _userManager = userManager;

        public async ValueTask<OperationResult<Order>> PlaceOrder(Order order)
        {
            await _unitOfWork.OrderRepository.AddOrderAsync(order);

            await _unitOfWork.CommitAsync();

            return order;
        }
        public async ValueTask<OperationResult<PagedResult<OrderInfo>>> GetPagedOrders(PagedRequest pagedRequest)
        {
            var orders = await _unitOfWork.OrderRepository.GetPagedOrdersAsync(pageIndex: pagedRequest.PageIndex, pageSize: pagedRequest.PageSize);
            PagedResult<OrderInfo> pagedOrderInfo = new PagedResult<OrderInfo>(orders.Page.Select(c => new OrderInfo
            {
                OrderId = c.Id,
                OrderName = c.OrderName,
                UserId = c.UserId,
                UserName = c.User.UserName
            }).ToList(),
                orders.TotalCount,
                orders.PageCount,
                orders.PageIndex,
                orders.PageSize);
            return OperationResult<PagedResult<OrderInfo>>.SuccessResult(pagedOrderInfo);
        }
    }
}
