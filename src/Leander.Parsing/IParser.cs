namespace Leander.Parsing;

public interface IParser<T>
{
    public bool TryParse(string input, out T result);
}