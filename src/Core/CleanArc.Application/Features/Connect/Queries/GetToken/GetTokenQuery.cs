using CleanArc.Domain.Models.Jwt;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;

namespace CleanArc.Application.Features.Connect.Queries.GetToken;

public record GetTokenQuery(string UserName, string Password) : IRequest<OperationResult<AccessToken>>,
    IValidatableModel<GetTokenQuery>
{
    public IValidator<GetTokenQuery> ValidateApplicationModel(ApplicationBaseValidationModelProvider<GetTokenQuery> validator)
    {
        validator.RuleFor(c => c.UserName)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter admin username");

        validator.RuleFor(c => c.Password)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter admin password");

        return validator;
    }
};