namespace Leander.Parsing.Tests;

public class UInt64ConverterTests
{
    [Theory]
    [InlineData("0", 0UL)]
    [InlineData("18446744073709551615", ulong.MaxValue)]
    public void TryParse_ValidInput_ReturnsExpected(string input, ulong expected)
    {
        var success = Converters.UInt64.TryParse(input, out var result);

        Assert.True(success);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("")]
    [InlineData("-1")]
    public void TryParse_InvalidInput_ReturnsFalse(string input)
    {
        Assert.False(Converters.UInt64.TryParse(input, out _));
    }

    [Fact]
    public void RoundTrips() => TestHelpers.AssertRoundTrip(Converters.UInt64, 123456789UL);
}
