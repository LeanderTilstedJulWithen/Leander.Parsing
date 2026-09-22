namespace Leander.Parsing.Tests;

public class ListConverterTests
{
    [Fact]
    public void TryParse_CommaSeparated_ReturnsElements()
    {
        var converter = Converters.List(Converters.Int32);

        var success = converter.TryParse("1,2,3", out var result);

        Assert.True(success);
        Assert.Equal(new[] { 1, 2, 3 }, result);
    }

    [Fact]
    public void TryParse_TrimsWhitespaceAroundElements()
    {
        var converter = Converters.List(Converters.Int32);

        var success = converter.TryParse(" 1, 2 , 3 ", out var result);

        Assert.True(success);
        Assert.Equal(new[] { 1, 2, 3 }, result);
    }

    [Fact]
    public void TryParse_EmptyInput_ReturnsEmptyList()
    {
        var converter = Converters.List(Converters.Int32);

        var success = converter.TryParse("", out var result);

        Assert.True(success);
        Assert.Empty(result);
    }

    [Fact]
    public void TryParse_OneInvalidElement_FailsWholeParse()
    {
        var converter = Converters.List(Converters.Int32);

        Assert.False(converter.TryParse("1,not-a-number,3", out _));
    }

    [Fact]
    public void TryParse_CustomDelimiter_Works()
    {
        var converter = Converters.List(Converters.Int32, delimiter: ';');

        var success = converter.TryParse("1;2;3", out var result);

        Assert.True(success);
        Assert.Equal(new[] { 1, 2, 3 }, result);
    }

    [Fact]
    public void Format_JoinsWithDelimiter()
    {
        var converter = Converters.List(Converters.Int32);

        Assert.Equal("1,2,3", converter.Format(new[] { 1, 2, 3 }));
    }

    [Fact]
    public void RoundTrips()
    {
        IReadOnlyList<int> value = [1, 2, 3];
        TestHelpers.AssertRoundTrip(Converters.List(Converters.Int32), value);
    }
}
