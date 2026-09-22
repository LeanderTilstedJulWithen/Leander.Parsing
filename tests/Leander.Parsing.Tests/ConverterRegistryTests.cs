namespace Leander.Parsing.Tests;

public class ConverterRegistryTests
{
    private enum Sample { First, Second }

    [Fact]
    public void RegisterDefaults_ResolvesBuiltInConverters()
    {
        var registry = new ConverterRegistryBuilder().RegisterDefaults().Build();

        Assert.Same(Converters.Int32, registry.GetConverter<int>());
        Assert.Same(Converters.String, registry.GetConverter<string>());
    }

    [Fact]
    public void GetConverter_KeyedVariant_ResolvesCorrectInstance()
    {
        var registry = new ConverterRegistryBuilder().RegisterDefaults().Build();

        Assert.Same(Converters.DateTimeLocal, registry.GetConverter<DateTime>(key: "Local"));
        Assert.Same(Converters.Int32Hex, registry.GetConverter<int>(key: "Hex"));
    }

    [Fact]
    public void GetConverter_UnregisteredType_Throws()
    {
        var registry = new ConverterRegistryBuilder().Build();

        Assert.Throws<KeyNotFoundException>(() => registry.GetConverter<int>());
    }

    [Fact]
    public void TryGetConverter_UnregisteredType_ReturnsFalse()
    {
        var registry = new ConverterRegistryBuilder().Build();

        var success = registry.TryGetConverter<int>(out var converter);

        Assert.False(success);
        Assert.Null(converter);
    }

    [Fact]
    public void GetConverter_EnumFallback_ResolvesWithoutExplicitRegistration()
    {
        var registry = new ConverterRegistryBuilder().RegisterDefaults().Build();

        var converter = registry.GetConverter<Sample>();
        var success = converter.TryParse("First", out var result);

        Assert.True(success);
        Assert.Equal(Sample.First, result);
    }

    [Fact]
    public void GetConverter_EnumFallback_DoesNotApplyToKeyedLookup()
    {
        var registry = new ConverterRegistryBuilder().RegisterDefaults().Build();

        Assert.Throws<KeyNotFoundException>(() => registry.GetConverter<Sample>(key: "Custom"));
    }

    [Fact]
    public void RegisterFallback_IsUsedOnMiss()
    {
        var builder = new ConverterRegistryBuilder();
        builder.RegisterFallback((_, type, _) => type == typeof(int) ? Converters.Int32 : null);
        var registry = builder.Build();

        Assert.Same(Converters.Int32, registry.GetConverter<int>());
    }

    [Fact]
    public void Register_ExplicitRegistration_ResolvesUnderGivenKey()
    {
        var custom = Converters.List(Converters.Int32, delimiter: ';');
        var registry = new ConverterRegistryBuilder()
            .RegisterDefaults()
            .Register(custom, key: "Custom")
            .Build();

        Assert.Same(custom, registry.GetConverter<IReadOnlyList<int>>(key: "Custom"));
    }
}
