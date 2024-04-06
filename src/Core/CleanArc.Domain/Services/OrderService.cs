﻿using Azure.Core;
using CleanArc.Domain.Contracts;
using CleanArc.Domain.Contracts.Identity;
using CleanArc.Domain.Contracts.Persistence;
using CleanArc.Domain.Entities.Order;
using CleanArc.Domain.Entities.User;
using CleanArc.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Services
{
    public class OrderService : IOrderContract
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppUserManager _userManager;

        public OrderService(IUnitOfWork unitOfWork, IAppUserManager userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async ValueTask<OperationResult<Order>> PlaceOrder(Order order)
        {
            await _unitOfWork.OrderRepository.AddOrderAsync(order);

            await _unitOfWork.CommitAsync();

            return order;
        }
    }
}
