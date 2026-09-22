namespace Leander.Parsing;

public sealed class ConverterRegistry
{
    private readonly IReadOnlyDictionary<(Type Type, string? Key), object> _converters;
    private readonly IReadOnlyList<Func<ConverterRegistry, Type, string?, object?>> _fallbacks;

    internal ConverterRegistry(
        IReadOnlyDictionary<(Type Type, string? Key), object> converters,
        IReadOnlyList<Func<ConverterRegistry, Type, string?, object?>> fallbacks)
    {
        _converters = converters;
        _fallbacks = fallbacks;
    }

    public IConverter<T> GetConverter<T>(string? key = null)
    {
        if (_converters.TryGetValue((typeof(T), key), out var value))
        {
            return (IConverter<T>)value;
        }

        if (ResolveFallback<T>(key) is { } fallbackConverter)
        {
            return fallbackConverter;
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

        converter = ResolveFallback<T>(key);
        return converter is not null;
    }

    private IConverter<T>? ResolveFallback<T>(string? key)
    {
        foreach (var fallback in _fallbacks)
        {
            if (fallback(this, typeof(T), key) is { } converter)
            {
                return (IConverter<T>)converter;
            }
        }

        return null;
    }
}
