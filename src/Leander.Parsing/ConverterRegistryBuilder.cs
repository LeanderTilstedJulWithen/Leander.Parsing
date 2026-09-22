using System.Reflection;
using Leander.Parsing.Internal;

namespace Leander.Parsing;

public sealed class ConverterRegistryBuilder
{
    private readonly Dictionary<(Type Type, string? Key), object> _converters = [];
    private readonly List<Func<ConverterRegistry, Type, string?, object?>> _fallbacks = [];

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
        .Register(Converters.DateTimeOffset)
        .RegisterFallback(EnumFallback);
    }

    public ConverterRegistryBuilder Register<T>(IConverter<T> converter, string? key = null)
    {
        _converters[(typeof(T), key)] = converter;
        return this;
    }

    public ConverterRegistryBuilder RegisterFallback(Func<ConverterRegistry, Type, string?, object?> fallback)
    {
        _fallbacks.Add(fallback);
        return this;
    }

    public ConverterRegistry Build() => new(
        new Dictionary<(Type Type, string? Key), object>(_converters),
        [.._fallbacks]);

    private static object? EnumFallback(ConverterRegistry registry, Type type, string? key)
    {
        if (key is not null || !type.IsEnum)
        {
            return null;
        }

        var cacheType = typeof(EnumConverterCache<>).MakeGenericType(type);
        var field = cacheType.GetField(nameof(EnumConverterCache<DayOfWeek>.Instance), BindingFlags.Public | BindingFlags.Static)!;
        return field.GetValue(null);
    }
}
