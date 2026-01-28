using Calculator.BusinessLogic.Serialization;
using Calculator.BusinessLogic.Validations;
using FluentAssertions;

namespace Calculator.BusinessLogic.Tests;

public sealed class Step3_CalculatorServiceTests
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
        };

        return new CalculatorService(options, serializer, validators);
    }

    [Fact]
    public void Resolve_NewlineDelimiter_ReturnsSum()
    {
        var service = CreateService();

        var result = service.Resolve("1\n2");

        result.Should().Be(3);
    }

    [Fact]
    public void Resolve_NewlineAndCommaDelimiters_CanBeMixed()
    {
        var service = CreateService();

        var result = service.Resolve("1\n2,3");

        result.Should().Be(6);
    }

    [Fact]
    public void Resolve_OnlyNewlineDelimiter_ReturnsSum()
    {
        var service = CreateService();

        var result = service.Resolve("4\n-3");

        result.Should().Be(1);
    }

    [Fact]
    public void Resolve_EmptyLineBetweenNumbers_IsTreatedAsZero()
    {
        var service = CreateService();

        var result = service.Resolve("5\n\n");

        result.Should().Be(5);
    }
}