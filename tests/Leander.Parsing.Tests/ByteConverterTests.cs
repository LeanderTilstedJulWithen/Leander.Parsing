namespace Leander.Parsing.Tests;

public class ByteConverterTests
{
    [Theory]
    [InlineData("0", (byte)0)]
    [InlineData("255", byte.MaxValue)]
    public void TryParse_ValidInput_ReturnsExpected(string input, byte expected)
    {
        var success = Converters.Byte.TryParse(input, out var result);

        Assert.True(success);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("")]
    [InlineData("256")]
    [InlineData("-1")]
    public void TryParse_InvalidInput_ReturnsFalse(string input)
    {
        Assert.False(Converters.Byte.TryParse(input, out _));
    }

    [Fact]
    public void RoundTrips() => TestHelpers.AssertRoundTrip(Converters.Byte, (byte)42);
}
