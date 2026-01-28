using Calculator.BusinessLogic.Serialization;
using Calculator.BusinessLogic.Validations;
using FluentAssertions;

namespace Calculator.BusinessLogic.Tests;

public sealed class Step5_CalculatorServiceTests
{
    CalculatorService CreateService()
    {
        var options = new CalculatorOptions();

        var serializer = new DelimitedListSerializer
        {
            Delimiters = [",", "\n"]
        };

        var validators = new IListValidator[]
        {
            new NegativeNotAllowedValidator(),
            new UpperBoundNormalizer()
        };

        return new CalculatorService(options, serializer, validators);
    }

    [Fact]
    public void Resolve_ValueGreaterThanUpperBound_IsIgnored()
    {
        var service = CreateService();

        var result = service.Resolve("2,1001,6");

        result.Should().Be(8);
    }

    [Fact]
    public void Resolve_ValueEqualToUpperBound_IsIncluded()
    {
        var service = CreateService();

        var result = service.Resolve("1000,1");

        result.Should().Be(1001);
    }

    [Fact]
    public void Resolve_AllValuesGreaterThanUpperBound_ReturnsZero()
    {
        var service = CreateService();

        var result = service.Resolve("1001,2000");

        result.Should().Be(0);
    }

    [Fact]
    public void Resolve_MixedValidInvalidAndUpperBoundValues_ReturnsCorrectSum()
    {
        var service = CreateService();

        var result = service.Resolve("5,tytyt,1001");

        result.Should().Be(5);
    }
}