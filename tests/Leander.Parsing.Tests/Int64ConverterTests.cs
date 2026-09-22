namespace Leander.Parsing.Tests;

public class Int64ConverterTests
{
    [Theory]
    [InlineData("0", 0L)]
    [InlineData("-9223372036854775808", long.MinValue)]
    [InlineData("9223372036854775807", long.MaxValue)]
    public void TryParse_ValidInput_ReturnsExpected(string input, long expected)
    {
        var success = Converters.Int64.TryParse(input, out var result);

        Assert.True(success);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("")]
    [InlineData("not a number")]
    public void TryParse_InvalidInput_ReturnsFalse(string input)
    {
        Assert.False(Converters.Int64.TryParse(input, out _));
    }

    [Fact]
    public void RoundTrips() => TestHelpers.AssertRoundTrip(Converters.Int64, -123456789L);
}
