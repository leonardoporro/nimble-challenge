using Calculator.BusinessLogic.Serialization;
using Calculator.BusinessLogic.Validations;
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
        };

        return new CalculatorService(serializer, validators);
    }

    [Fact]
    public void Add_SingleNumber_ReturnsThatNumber()
    {
        var service = CreateService();

        var result = service.Resolve("20");

        result.Should().Be(20);
    }

    [Fact]
    public void Add_TwoNumbers_ReturnsSum()
    {
        var service = CreateService();

        var result = service.Resolve("1,5000");

        result.Should().Be(5001);
    }

    [Fact]
    public void Add_EmptyInput_ReturnsZero()
    {
        var service = CreateService();

        var result = service.Resolve(string.Empty);

        result.Should().Be(0);
    }

    [Fact]
    public void Add_MissingSecondNumber_IsConvertedToZero()
    {
        var service = CreateService();

        var result = service.Resolve("5,");

        result.Should().Be(5);
    }

    [Fact]
    public void Add_InvalidNumber_IsConvertedToZero()
    {
        var service = CreateService();

        var result = service.Resolve("5,tytyt");

        result.Should().Be(5);
    }

    [Fact]
    public void Add_MoreThanTwoNumbers_Returns()
    {
        var service = CreateService();

        var result = service.Resolve("1,2,3,4,5,6,7,8,9,10,11,12");

        result.Should().Be(78);
    }
}