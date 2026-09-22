using System.Reflection;
using Leander.Parsing.Internal;

namespace Leander.Parsing;

public sealed class ConverterRegistry
{
    private readonly IReadOnlyDictionary<(Type Type, string? Key), object> _converters;

    internal ConverterRegistry(IReadOnlyDictionary<(Type Type, string? Key), object> converters)
    {
        _converters = converters;
    }

    public IConverter<T> GetConverter<T>(string? key = null)
    {
        if (_converters.TryGetValue((typeof(T), key), out var value))
        {
            return (IConverter<T>)value;
        }

        if (key is null && TryCreateEnumConverter<T>() is { } enumConverter)
        {
            return enumConverter;
        }

        return (IConverter<T>)_converters[(typeof(T), key)];
    }

    public bool TryGetConverter<T>(out IConverter<T>? converter, string? key = null)
    {
        if (_converters.TryGetValue((typeof(T), key), out var value))
        {
            converter = (IConverter<T>)value;
            return true;
        }

        converter = key is null ? TryCreateEnumConverter<T>() : null;
        return converter is not null;
    }

    private static IConverter<T>? TryCreateEnumConverter<T>()
    {
        if (!typeof(T).IsEnum)
        {
            return null;
        }

        var cacheType = typeof(EnumConverterCache<>).MakeGenericType(typeof(T));
        var field = cacheType.GetField(nameof(EnumConverterCache<DayOfWeek>.Instance), BindingFlags.Public | BindingFlags.Static)!;
        return (IConverter<T>)field.GetValue(null)!;
    }
}
