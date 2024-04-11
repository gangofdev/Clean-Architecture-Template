using CleanArc.Application.Profiles;
using CleanArc.Domain.Entities.User;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using System.Text.RegularExpressions;

namespace CleanArc.Application.Features.Connect.Commands.Create;

public record SignupCommand
    (string UserName, string Name, string FamilyName, string Password, string PhoneNumber) 
    : IRequest<OperationResult<SignupCommandResult>>
        ,IValidatableModel<SignupCommand>
,ICreateMapper<User>
{

    public IValidator<SignupCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<SignupCommand> validator)
    {

        validator
            .RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("User must have first name");

        validator.RuleFor(c => c.UserName)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter your username");

        validator
            .RuleFor(c => c.FamilyName)
            .NotEmpty()
            .NotNull()
            .WithMessage("User must have last name");

        validator
           .RuleFor(c => c.Password)
           .NotEmpty()
           .NotNull()
           .WithMessage("User must have last name");

        validator.RuleFor(c => c.PhoneNumber).NotEmpty()
            .NotNull().WithMessage("Phone Number is required.")
            .MinimumLength(10).WithMessage("PhoneNumber must not be less than 10 characters.")
            .MaximumLength(20).WithMessage("PhoneNumber must not exceed 50 characters.")
            .Matches(new Regex(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")).WithMessage("Phone number is not valid");

        return validator;
    }
    /// <summary>
    /// Custom Profile Mapper
    /// </summary>
    /// <param name="profile"></param>
    void ICreateMapper<User>.Map(AutoMapper.Profile profile)
    {
        profile.CreateMap<SignupCommand, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.FamilyName, opt => opt.MapFrom(src => src.FamilyName))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber)).ReverseMap();
    }


}