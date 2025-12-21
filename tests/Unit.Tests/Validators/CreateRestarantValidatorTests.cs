using FluentValidation.TestHelper;
using Revision_Part_1.Application.CQRS.Commands;
using Revision_Part_1.Application.DTOs;
using Revision_Part_1.Application.Validators;

namespace Unit.Tests.Validators;

public class CreateRestarantValidatorTests
{
    [Fact()]
    public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
    {
        //Arrange
        var command = new CreateRestaurantCommand()
        {
            CreateRestaurantDto = new CreateRestaurantDto
            {
                Name = "Valid Restaurant",
                Category = "Italian",
                ContactEmail = "test@test.com",
                PostalCode = "12-345"
            }
        };
        var validator = new CreateRestaurantValidation();

        //Act
        var result = validator.TestValidate(command);

        //Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact()]
    public void Validator_ForInvalidCommand_ShouldHaveValidationErrors()
    {
        //Arrange
        var command = new CreateRestaurantCommand()
        {
            CreateRestaurantDto = new CreateRestaurantDto
            {
                Name = "Va",
                Category = "Ita",
                ContactEmail = "@test.com",
                PostalCode = "12345"
            }
        };
        var validator = new CreateRestaurantValidation();

        //Act
        var result = validator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(c => c.CreateRestaurantDto!.Name);
        result.ShouldHaveValidationErrorFor(c => c.CreateRestaurantDto!.Category);
        result.ShouldHaveValidationErrorFor(c => c.CreateRestaurantDto!.ContactEmail);
        result.ShouldHaveValidationErrorFor(c => c.CreateRestaurantDto!.PostalCode);
    }

    [Theory]
    [InlineData("Italian")]
    [InlineData("Maxican")]
    [InlineData("Japanese")]
    [InlineData("American")]
    [InlineData("Indian")]
    public void Validator_ForValidCategories_ShouldNotHaveValidationErrors(string category)
    {
        //Arrange
        var validator = new CreateRestaurantValidation();
        var command = new CreateRestaurantCommand()
        {
            CreateRestaurantDto = new CreateRestaurantDto
            {
                Category = category,                
            }
        };

        //Act
        var result = validator.TestValidate(command);

        //Assert
        result.ShouldNotHaveValidationErrorFor(c => c.CreateRestaurantDto!.Category);
    }

    [Theory]
    [InlineData("10220")]
    [InlineData("102-20")]
    [InlineData("10 220")]
    [InlineData("10-2 20")]
    public void Validator_ForValidPostalCode_ShouldNotHaveValidationErrors(string postalCode)
    {
        //Arrange
        var validator = new CreateRestaurantValidation();
        var command = new CreateRestaurantCommand()
        {
            CreateRestaurantDto = new CreateRestaurantDto
            {
                PostalCode = postalCode,
            }
        };

        //Act
        var result = validator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(c => c.CreateRestaurantDto!.PostalCode);
    }
}
