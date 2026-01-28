using Calculator.BusinessLogic.Serialization;
using Calculator.BusinessLogic.Validations;
using FluentAssertions;

namespace Calculator.BusinessLogic.Tests;

public sealed class Step7_CalculatorServiceTests
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
    public void Resolve_WithLongCustomDelimiter_ReturnsSum()
    {
        var service = CreateService();

        var result = service.Resolve("//[***]\n11***22***33");

        result.Should().Be(66);
    }

    [Fact]
    public void Resolve_LongCustomDelimiter_WithInvalidValues_IgnoresThem()
    {
        var service = CreateService();

        var result = service.Resolve("//[abc]\n1abcfooabc3");

        result.Should().Be(4);
    }

    [Fact]
    public void Resolve_LongCustomDelimiter_DoesNotBreakPreviousSingleCharFormat()
    {
        var service = CreateService();

        var result = service.Resolve("//#\n2#5");

        result.Should().Be(7);
    }

    [Fact]
    public void Resolve_LongCustomDelimiter_DoesNotBreakDefaultDelimiters()
    {
        var service = CreateService();

        var result = service.Resolve("1\n2,3");

        result.Should().Be(6);
    }
}