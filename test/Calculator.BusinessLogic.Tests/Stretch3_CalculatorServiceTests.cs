using Calculator.BusinessLogic.Serialization;
using Calculator.BusinessLogic.Validations;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Calculator.BusinessLogic.Tests;

public sealed class Stretch3_CalculatorServiceTests
{
    [Fact]
    public void AddCalculator_RegistersCalculatorService()
    {
        var services = new ServiceCollection();

        services.AddCalculator();

        var provider = services.BuildServiceProvider();
        var service = provider.GetService<CalculatorService>();

        service.Should().NotBeNull();
    }

    [Fact]
    public void AddCalculator_RegistersOptions_AsSingleton()
    {
        var services = new ServiceCollection();
        var options = new CalculatorOptions
        {
            AllowNegativeNumbers = false,
            UpperBound = 500
        };

        services.AddCalculator(options);

        var provider = services.BuildServiceProvider();
        var resolvedOptions = provider.GetRequiredService<CalculatorOptions>();

        resolvedOptions.Should().BeSameAs(options);
    }

    [Fact]
    public void AddCalculator_RegistersSerializer_WithConfiguredDelimiters()
    {
        var services = new ServiceCollection();
        var options = new CalculatorOptions
        {
            AlternateDelimiter = ";"
        };

        services.AddCalculator(options);

        var provider = services.BuildServiceProvider();
        var serializer = provider.GetRequiredService<IListSerializer>()
                         as DelimitedListSerializer;

        serializer.Should().NotBeNull();
        serializer!.Delimiters.Should().Contain(new[] { ",", ";" });
    }

    [Fact]
    public void AddCalculator_WhenNegativeNumbersNotAllowed_RegistersNegativeValidator()
    {
        var services = new ServiceCollection();
        var options = new CalculatorOptions
        {
            AllowNegativeNumbers = false
        };

        services.AddCalculator(options);

        var provider = services.BuildServiceProvider();
        var validators = provider.GetServices<IListValidator>();

        validators.Should().ContainSingle(v =>
            v.GetType() == typeof(NegativeNotAllowedValidator));
    }

    [Fact]
    public void AddCalculator_WhenNegativeNumbersAllowed_DoesNotRegisterNegativeValidator()
    {
        var services = new ServiceCollection();
        var options = new CalculatorOptions
        {
            AllowNegativeNumbers = true
        };

        services.AddCalculator(options);

        var provider = services.BuildServiceProvider();
        var validators = provider.GetServices<IListValidator>();

        validators.Should().NotContain(v =>
            v.GetType() == typeof(NegativeNotAllowedValidator));
    }

    [Fact]
    public void AddCalculator_AlwaysRegistersUpperBoundNormalizer_WithConfiguredValue()
    {
        var services = new ServiceCollection();
        var options = new CalculatorOptions
        {
            UpperBound = 123
        };

        services.AddCalculator(options);

        var provider = services.BuildServiceProvider();
        var normalizer = provider
            .GetServices<IListValidator>()
            .OfType<UpperBoundNormalizer>()
            .Single();

        normalizer.UpperBoundValue.Should().Be(123);
    }
}
