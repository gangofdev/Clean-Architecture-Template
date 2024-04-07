using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using System.Text.RegularExpressions;

namespace CleanArc.Application.Features.Connect.Queries.GetUserInfo;

public record GetUserInfoRequestQuery() : IRequest<OperationResult<GetUserInfoRequestQueryResponse>>
{
    
};