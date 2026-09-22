namespace Leander.Parsing;

public interface IConverter<T> : IParser<T>, IFormatter<T>
{
}
