using Calculator.BusinessLogic.Serialization;
using Calculator.BusinessLogic.Validations;
using Calculator.BusinessLogic.Validations.Exceptions;
using FluentAssertions;

namespace Calculator.BusinessLogic.Tests;

public sealed class Step1_CalculatorServiceTests
{
    CalculatorService CreateService()
    {
        var serializer = new DelimitedListSerializer
        {
            Delimiters = [ ",", "\n" ]
        };

        var validators = new IListValidator[]
        {
            new UpperCountValidator()
        };

        return new CalculatorService(serializer, validators);
    }

    [Fact]
    public void Resolve_SingleNumber_ReturnsThatNumber()
    {
        var service = CreateService();

        var result = service.Resolve("20");

        result.Should().Be(20);
    }

    [Fact]
    public void Resolve_TwoNumbers_ReturnsSum()
    {
        var service = CreateService();

        var result = service.Resolve("1,5000");

        result.Should().Be(5001);
    }

    [Fact]
    public void Resolve_EmptyInput_ReturnsZero()
    {
        var service = CreateService();

        var result = service.Resolve(string.Empty);

        result.Should().Be(0);
    }

    [Fact]
    public void Resolve_MissingSecondNumber_IsConvertedToZero()
    {
        var service = CreateService();

        var result = service.Resolve("5,");

        result.Should().Be(5);
    }

    [Fact]
    public void Resolve_InvalidNumber_IsConvertedToZero()
    {
        var service = CreateService();

        var result = service.Resolve("5,tytyt");

        result.Should().Be(5);
    }

    [Fact]
    public void Resolve_MoreThanTwoNumbers_ThrowsException()
    {
        var service = CreateService();

        Action act = () => service.Resolve("1,2,3");

        act.Should().Throw<ItemCountException>()
           .WithMessage("*3*2*");
    }
}