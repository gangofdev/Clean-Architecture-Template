﻿using CleanArc.Domain.Models.Jwt;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;

namespace CleanArc.Application.Features.Users.Queries.GenerateUserToken;

public record GenerateUserTokenQuery(string UserKey, string Code) : IRequest<OperationResult<AccessTokenResponse>>,
    IValidatableModel<GenerateUserTokenQuery>
{
    public IValidator<GenerateUserTokenQuery> ValidateApplicationModel(ApplicationBaseValidationModelProvider<GenerateUserTokenQuery> validator)
    {
        validator.RuleFor(c => c.Code)
            .NotEmpty()
            .NotNull()
            .Length(6)
            .WithMessage("User code is not valid");

        validator.RuleFor(c => c.UserKey)
            .NotEmpty()
            .NotNull()
            .WithMessage("Invalid user key");

        return validator;
    }
};