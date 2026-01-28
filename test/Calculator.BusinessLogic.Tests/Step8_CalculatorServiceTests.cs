using Calculator.BusinessLogic.Serialization;
using Calculator.BusinessLogic.Validations;
using FluentAssertions;

namespace Calculator.BusinessLogic.Tests;

public sealed class Step8_CalculatorServiceTests
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
    public void Resolve_WithMultipleCustomDelimiters_ReturnsSum()
    {
        var service = CreateService();

        var result = service.Resolve("//[*][!!][r9r]\n11r9r22*hh*33!!44");

        result.Should().Be(110);
    }

    [Fact]
    public void Resolve_MultipleDelimiters_WithInvalidValues_IgnoresThem()
    {
        var service = CreateService();

        var result = service.Resolve("//[aa][bbb]\n1aafooaa2bbb3");

        result.Should().Be(6);
    }

    [Fact]
    public void Resolve_MultipleDelimiters_DoesNotBreakSingleDelimiter()
    {
        var service = CreateService();

        var result = service.Resolve("//[***]\n11***22***33");

        result.Should().Be(66);
    }

    [Fact]
    public void Resolve_MultipleDelimiters_DoesNotBreakDefaultDelimiters()
    {
        var service = CreateService();

        var result = service.Resolve("1\n2,3");

        result.Should().Be(6);
    }
}