namespace Leander.Parsing.Tests;

public class DictionaryConverterTests
{
    [Fact]
    public void TryParse_KeyValuePairs_ReturnsEntries()
    {
        var converter = Converters.Dictionary(Converters.String, Converters.Int32);

        var success = converter.TryParse("a=1,b=2", out var result);

        Assert.True(success);
        Assert.Equal(1, result["a"]);
        Assert.Equal(2, result["b"]);
    }

    [Fact]
    public void TryParse_ValueContainingDelimiter_SplitsOnFirstOccurrenceOnly()
    {
        var converter = Converters.Dictionary(Converters.String, Converters.String);

        var success = converter.TryParse("path=/a=b", out var result);

        Assert.True(success);
        Assert.Equal("/a=b", result["path"]);
    }

    [Fact]
    public void TryParse_DuplicateKey_FailsWholeParse()
    {
        var converter = Converters.Dictionary(Converters.String, Converters.Int32);

        Assert.False(converter.TryParse("a=1,a=2", out _));
    }

    [Fact]
    public void TryParse_MissingDelimiter_FailsWholeParse()
    {
        var converter = Converters.Dictionary(Converters.String, Converters.Int32);

        Assert.False(converter.TryParse("a=1,b", out _));
    }

    [Fact]
    public void TryParse_EmptyInput_ReturnsEmptyDictionary()
    {
        var converter = Converters.Dictionary(Converters.String, Converters.Int32);

        var success = converter.TryParse("", out var result);

        Assert.True(success);
        Assert.Empty(result);
    }

    [Fact]
    public void RoundTrips()
    {
        var converter = Converters.Dictionary(Converters.String, Converters.Int32);
        IReadOnlyDictionary<string, int> value = new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 };

        TestHelpers.AssertRoundTrip(converter, value);
    }
}
