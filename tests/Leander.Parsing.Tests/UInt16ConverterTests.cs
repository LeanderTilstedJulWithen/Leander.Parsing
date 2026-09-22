namespace Leander.Parsing.Tests;

public class UInt16ConverterTests
{
    [Theory]
    [InlineData("0", (ushort)0)]
    [InlineData("65535", ushort.MaxValue)]
    public void TryParse_ValidInput_ReturnsExpected(string input, ushort expected)
    {
        var success = Converters.UInt16.TryParse(input, out var result);

        Assert.True(success);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("")]
    [InlineData("65536")]
    [InlineData("-1")]
    public void TryParse_InvalidInput_ReturnsFalse(string input)
    {
        Assert.False(Converters.UInt16.TryParse(input, out _));
    }

    [Fact]
    public void RoundTrips() => TestHelpers.AssertRoundTrip(Converters.UInt16, (ushort)1234);
}
