using Calculator.BusinessLogic;
using Calculator.BusinessLogic.Serialization;
using Calculator.BusinessLogic.Validations;
using FluentAssertions;

public sealed class Stretch3_CalculatorServiceTests
{
    CalculatorService CreateService()
    {
        var serializer = new DelimitedListSerializer
        {
            Delimiters = [",", "\n"]
        };

        var validators = new IListValidator[]
        {
            new NegativeNotAllowedValidator(),
            new UpperBoundNormalizer()
        };

        return new CalculatorService(serializer, validators);
    }

    [Fact]
    public void Resolve_WithExplain_ReturnsExactExplanationString()
    {
        var service = CreateService();

        var value = service.Resolve("2,,4,rrrr,1001,6", out var explain);

        value.Should().Be(12);

        explain.Should().Be(
            "2++4+rrrr+1001+6 => 2+0+4+0+0+6 = 12"
        );
    }
}