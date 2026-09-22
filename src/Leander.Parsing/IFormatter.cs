namespace Leander.Parsing;

public interface IFormatter<T>
{
    public string Format(T value);
}