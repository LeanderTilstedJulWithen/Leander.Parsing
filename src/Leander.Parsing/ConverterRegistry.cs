namespace Leander.Parsing;

public sealed class ConverterRegistry
{
    private readonly IReadOnlyDictionary<(Type Type, string? Key), object> _converters;

    internal ConverterRegistry(IReadOnlyDictionary<(Type Type, string? Key), object> converters)
    {
        _converters = converters;
    }

    public IConverter<T> GetConverter<T>(string? key = null) => (IConverter<T>)_converters[(typeof(T), key)];

    public bool TryGetConverter<T>(out IConverter<T>? converter, string? key = null)
    {
        if (_converters.TryGetValue((typeof(T), key), out var value))
        {
            converter = (IConverter<T>)value;
            return true;
        }

        converter = null;
        return false;
    }
}
