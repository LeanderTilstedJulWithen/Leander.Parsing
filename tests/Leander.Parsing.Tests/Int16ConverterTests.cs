namespace Leander.Parsing.Tests;

public class Int16ConverterTests
{
    [Theory]
    [InlineData("0", (short)0)]
    [InlineData("-32768", short.MinValue)]
    [InlineData("32767", short.MaxValue)]
    public void TryParse_ValidInput_ReturnsExpected(string input, short expected)
    {
        var success = Converters.Int16.TryParse(input, out var result);

        Assert.True(success);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("")]
    [InlineData("32768")]
    [InlineData("-32769")]
    public void TryParse_InvalidInput_ReturnsFalse(string input)
    {
        Assert.False(Converters.Int16.TryParse(input, out _));
    }

    [Fact]
    public void RoundTrips() => TestHelpers.AssertRoundTrip(Converters.Int16, (short)-1234);
}
