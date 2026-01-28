using Calculator.BusinessLogic.Serialization;
using Calculator.BusinessLogic.Validations;
using FluentAssertions;

namespace Calculator.BusinessLogic.Tests;

public sealed class Step6_CalculatorServiceTests
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
    public void Resolve_WithCustomSingleCharDelimiter_ReturnsSum()
    {
        var service = CreateService();

        var result = service.Resolve("//#\n2#5");

        result.Should().Be(7);
    }

    [Fact]
    public void Resolve_WithCustomCommaDelimiter_StillWorks()
    {
        var service = CreateService();

        var result = service.Resolve("//,\n2,ff,100");

        result.Should().Be(102);
    }

    [Fact]
    public void Resolve_CustomDelimiter_DoesNotBreakPreviousFormats()
    {
        var service = CreateService();

        var result = service.Resolve("1\n2,3");

        result.Should().Be(6);
    }

    [Fact]
    public void Resolve_CustomDelimiter_WithInvalidValues_IgnoresThem()
    {
        var service = CreateService();

        var result = service.Resolve("//;\n1;tytyt;3");

        result.Should().Be(4);
    }

    [Fact]
    public void Resolve_CustomDelimiter_WithUpperBound_IgnoresLargeValues()
    {
        var service = CreateService();

        var result = service.Resolve("//|\n2|1001|6");

        result.Should().Be(8);
    }
}