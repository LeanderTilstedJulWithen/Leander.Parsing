namespace Leander.Parsing.Tests;

public class Int32ConverterTests
{
    [Theory]
    [InlineData("0", 0)]
    [InlineData("42", 42)]
    [InlineData("-17", -17)]
    [InlineData("2147483647", int.MaxValue)]
    [InlineData("-2147483648", int.MinValue)]
    public void TryParse_ValidInput_ReturnsExpected(string input, int expected)
    {
        var success = Converters.Int32.TryParse(input, out var result);

        Assert.True(success);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("")]
    [InlineData("not a number")]
    [InlineData("3.14")]
    [InlineData("99999999999999999999")]
    public void TryParse_InvalidInput_ReturnsFalse(string input)
    {
        Assert.False(Converters.Int32.TryParse(input, out _));
    }

    [Fact]
    public void RoundTrips() => TestHelpers.AssertRoundTrip(Converters.Int32, -12345);
}
