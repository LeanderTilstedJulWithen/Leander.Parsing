namespace Leander.Parsing.Tests;

public class JsonConverterTests
{
    private sealed record Point(int X, int Y);

    [Fact]
    public void TryParse_ValidJson_ReturnsObject()
    {
        var converter = Converters.Json<Point>();

        var success = converter.TryParse("""{"X":1,"Y":2}""", out var result);

        Assert.True(success);
        Assert.Equal(new Point(1, 2), result);
    }

    [Fact]
    public void TryParse_InvalidJson_ReturnsFalse()
    {
        var converter = Converters.Json<Point>();

        Assert.False(converter.TryParse("not json", out _));
    }

    [Fact]
    public void RoundTrips() => TestHelpers.AssertRoundTrip(Converters.Json<Point>(), new Point(3, 4));
}
