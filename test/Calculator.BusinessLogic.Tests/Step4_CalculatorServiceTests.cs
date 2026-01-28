using Calculator.BusinessLogic.Serialization;
using Calculator.BusinessLogic.Validations;
using Calculator.BusinessLogic.Validations.Exceptions;
using FluentAssertions;

namespace Calculator.BusinessLogic.Tests;

public sealed class Step4_CalculatorServiceTests
{
    CalculatorService CreateService()
    {
        var serializer = new DelimitedListSerializer
        {
            Delimiters = [",", "\n"]
        };

        var validators = new IListValidator[]
        {
            new NegativeNotAllowedValidator()
        };

        return new CalculatorService(serializer, validators);
    }

    [Fact]
    public void Resolve_WithSingleNegative_ThrowsException()
    {
        var service = CreateService();

        Action act = () => service.Resolve("1,-2");

        act.Should().Throw<NegativeNotAllowedException>()
            .WithMessage("*: -2");
    }

    [Fact]
    public void Resolve_WithMultipleNegatives_ThrowsExceptionListingAll()
    {
        var service = CreateService();

        Action act = () => service.Resolve("-1,2,-3");

        act.Should().Throw<NegativeNotAllowedException>()
           .WithMessage("*: -1, -3");
    }

    [Fact]
    public void Resolve_WithOnlyNegatives_ThrowsExceptionListingAll()
    {
        var service = CreateService();

        Action act = () => service.Resolve("-1, -2");

        act.Should().Throw<NegativeNotAllowedException>()
           .WithMessage("*: -1, -2");
    }

    [Fact]
    public void Resolve_WithNoNegatives_ReturnsSum()
    {
        var service = CreateService();

        var result = service.Resolve("2,3");

        result.Should().Be(5);
    }
}