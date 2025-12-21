using FluentValidation;
using Revision_Part_1.Application.CQRS.Commands;
using Revision_Part_1.Application.DTOs;

namespace Revision_Part_1.Application.Validators;

//public class CreateRestaurantValidation : AbstractValidator<CreateRestaurantDto>
public class CreateRestaurantValidation : AbstractValidator<CreateRestaurantCommand>
{
    private readonly List<string> validCategories = ["Italian", "Maxican", "Japanese", "American", "Indian"];
    public CreateRestaurantValidation() {
        RuleFor(dto => dto.CreateRestaurantDto!.Name).Length(3, 100);

        //RuleFor(dto => dto.Description).NotEmpty().WithMessage("Description is required.....");
        //RuleFor(dto => dto.Category).NotEmpty().WithMessage("Category is required.....");
        //RuleFor(dto => dto.Category).Custom((value, context) =>
        //{
        //    var isValidCategory = validCategories.Contains(value);
        //    if (!isValidCategory)
        //        context.AddFailure("Category", "Invalid category. Please choose from valid category list provided to you.....");
        //});
        RuleFor(dto => dto.CreateRestaurantDto!.Category).Must(validCategories.Contains).WithMessage("Invalid category. Please choose from valid category list provided to you.....");

        RuleFor(dto => dto.CreateRestaurantDto!.ContactEmail).EmailAddress().WithMessage("Email address is required.....");
        RuleFor(dto => dto.CreateRestaurantDto!.PostalCode).Matches(@"^\d{2}-\d{3}$").WithMessage("Please provide a valid postal code (XX-XXX).....");
    }
}
