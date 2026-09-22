namespace Leander.Parsing;

public sealed class ConverterRegistryBuilder
{
    private readonly Dictionary<(Type Type, string? Key), object> _converters = [];

    public ConverterRegistryBuilder RegisterDefaults()
    {
        return Register(Converters.String)
        .Register(Converters.Int32)
        .Register(Converters.Double)
        .Register(Converters.DateTimeUtc)
        .Register(Converters.DateTimeLocal, key: "Local")
        .Register(Converters.DateTimeOffset);
    }

    public ConverterRegistryBuilder Register<T>(IConverter<T> converter, string? key = null)
    {
        _converters[(typeof(T), key)] = converter;
        return this;
    }

    public ConverterRegistry Build() => new(new Dictionary<(Type Type, string? Key), object>(_converters));
}
