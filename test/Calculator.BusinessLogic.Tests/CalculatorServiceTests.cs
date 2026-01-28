using Calculator.BusinessLogic.Serialization;
using Calculator.BusinessLogic.Validations;
using Calculator.BusinessLogic.Validations.Exceptions;
using FluentAssertions;

namespace Calculator.BusinessLogic.Tests;

public sealed class CalculatorServiceTests
{
    private CalculatorService CreateService()
    {
        var serializer = new DelimitedListSerializer
        {
            Delimiters = [ "," ]
        };

        var validators = new IListValidator[]
        {
            new UpperCountValidator()
        };

        return new CalculatorService(serializer, validators);
    }

    [Fact]
    public void Add_SingleNumber_ReturnsThatNumber()
    {
        var sut = CreateService();

        var result = sut.Resolve("20");

        result.Should().Be(20);
    }

    [Fact]
    public void Add_TwoNumbers_ReturnsSum()
    {
        var sut = CreateService();

        var result = sut.Resolve("1,5000");

        result.Should().Be(5001);
    }

    [Fact]
    public void Add_EmptyInput_ReturnsZero()
    {
        var sut = CreateService();

        var result = sut.Resolve(string.Empty);

        result.Should().Be(0);
    }

    [Fact]
    public void Add_MissingSecondNumber_IsConvertedToZero()
    {
        var sut = CreateService();

        var result = sut.Resolve("5,");

        result.Should().Be(5);
    }

    [Fact]
    public void Add_InvalidNumber_IsConvertedToZero()
    {
        var sut = CreateService();

        var result = sut.Resolve("5,tytyt");

        result.Should().Be(5);
    }

    [Fact]
    public void Add_MoreThanTwoNumbers_Throws()
    {
        var sut = CreateService();

        Action act = () => sut.Resolve("1,2,3");

        act.Should().Throw<ItemCountException>()
           .WithMessage("*3*2*");
    }
}