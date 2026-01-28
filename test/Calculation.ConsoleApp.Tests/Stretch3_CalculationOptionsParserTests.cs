using Calculator.ConsoleApp.Services;
using FluentAssertions;

namespace Calculator.BusinessLogic.Tests;

public class Stretch3_CalculationOptionsParserTests
{
    [Fact]
    public void FromArgs_WithNoArguments_UsesDefaults()
    {
        var options = CalculatorOptionsParser.FromArgs(Array.Empty<string>());

        options.AllowNegativeNumbers.Should().BeFalse();
        options.UpperBound.Should().Be(1000);
        options.AlternateDelimiter.Should().Be("\n");
        options.Operator.Should().Be("+");
    }

    [Fact]
    public void FromArgs_NoNegativesFlag_DisablesNegativeNumbers()
    {
        var options = CalculatorOptionsParser.FromArgs(
            new[] { "--no-negatives" });

        options.AllowNegativeNumbers.Should().BeFalse();
    }

    [Fact]
    public void FromArgs_UpperBoundArgument_SetsUpperBound()
    {
        var options = CalculatorOptionsParser.FromArgs(
            new[] { "--upper-bound=500" });

        options.UpperBound.Should().Be(500);
    }

    [Fact]
    public void FromArgs_InvalidUpperBound_KeepsDefault()
    {
        var options = CalculatorOptionsParser.FromArgs(
            new[] { "--upper-bound=abc" });

        options.UpperBound.Should().Be(1000);
    }

    [Fact]
    public void FromArgs_DelimiterArgument_SetsAlternateDelimiter()
    {
        var options = CalculatorOptionsParser.FromArgs(
            new[] { "--delimiter=;" });

        options.AlternateDelimiter.Should().Be(";");
    }

    [Theory]
    [InlineData("+")]
    [InlineData("*")]
    [InlineData("-")]
    [InlineData("/")]
    public void FromArgs_OperatorArgument_SetsOperator(string op)
    {
        var options = CalculatorOptionsParser.FromArgs(
            new[] { $"--operator={op}" });

        options.Operator.Should().Be(op);
    }

    [Fact]
    public void FromArgs_MultipleArguments_AllAreApplied()
    {
        var options = CalculatorOptionsParser.FromArgs(
            new[]
            {
                "--no-negatives",
                "--upper-bound=250",
                "--delimiter=#",
                "--operator=*"
            });

        options.AllowNegativeNumbers.Should().BeFalse();
        options.UpperBound.Should().Be(250);
        options.AlternateDelimiter.Should().Be("#");
        options.Operator.Should().Be("*");
    }

    [Fact]
    public void FromArgs_UnknownArguments_AreIgnored()
    {
        var options = CalculatorOptionsParser.FromArgs(
            new[] { "--unknown=whatever" });

        options.AllowNegativeNumbers.Should().BeFalse();
        options.UpperBound.Should().Be(1000);
        options.AlternateDelimiter.Should().Be("\n");
        options.Operator.Should().Be("+");
    }
}