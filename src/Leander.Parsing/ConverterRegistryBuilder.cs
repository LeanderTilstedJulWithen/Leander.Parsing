namespace Leander.Parsing;

public sealed class ConverterRegistryBuilder
{
    private readonly Dictionary<(Type Type, string? Key), object> _converters = [];

    public ConverterRegistryBuilder RegisterDefaults()
    {
        return Register(Converters.String)
        .Register(Converters.Boolean)
        .Register(Converters.Byte)
        .Register(Converters.SByte)
        .Register(Converters.Int16)
        .Register(Converters.UInt16)
        .Register(Converters.Int32)
        .Register(Converters.Int32Hex, key: "Hex")
        .Register(Converters.UInt32)
        .Register(Converters.UInt32Hex, key: "Hex")
        .Register(Converters.Int64)
        .Register(Converters.UInt64)
        .Register(Converters.Single)
        .Register(Converters.Double)
        .Register(Converters.Decimal)
        .Register(Converters.Guid)
        .Register(Converters.Uri)
        .Register(Converters.TimeSpan)
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
