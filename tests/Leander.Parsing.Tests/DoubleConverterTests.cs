namespace Leander.Parsing.Tests;

public class DoubleConverterTests
{
    [Theory]
    [InlineData("0", 0d)]
    [InlineData("3.14", 3.14d)]
    [InlineData("-2.5", -2.5d)]
    public void TryParse_ValidInput_ReturnsExpected(string input, double expected)
    {
        var success = Converters.Double.TryParse(input, out var result);

        Assert.True(success);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("")]
    [InlineData("not a number")]
    [InlineData("1,000")]
    public void TryParse_InvalidInput_ReturnsFalse(string input)
    {
        Assert.False(Converters.Double.TryParse(input, out _));
    }

    [Fact]
    public void RoundTrips() => TestHelpers.AssertRoundTrip(Converters.Double, 2.71828d);
}
