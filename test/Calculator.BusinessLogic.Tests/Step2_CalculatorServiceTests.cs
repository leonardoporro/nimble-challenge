using Calculator.BusinessLogic.Serialization;
using Calculator.BusinessLogic.Validations;
using FluentAssertions;

namespace Calculator.BusinessLogic.Tests;

public sealed class Step2_CalculatorServiceTests
{
    CalculatorService CreateService()
    {
        var serializer = new DelimitedListSerializer
        {
            Delimiters = [",", "\n"]
        };

        var validators = new IListValidator[]
        {
        };

        return new CalculatorService(serializer, validators);
    }

    [Fact]
    public void Resolve_MoreThanTwoNumbers_ReturnsSum()
    {
        var service = CreateService();

        var result = service.Resolve("1,2,3,4,5,6,7,8,9,10,11,12");

        result.Should().Be(78);
    }
}