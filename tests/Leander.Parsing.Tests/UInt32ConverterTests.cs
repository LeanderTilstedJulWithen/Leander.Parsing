namespace Leander.Parsing.Tests;

public class UInt32ConverterTests
{
    [Theory]
    [InlineData("0", 0u)]
    [InlineData("4294967295", uint.MaxValue)]
    public void TryParse_ValidInput_ReturnsExpected(string input, uint expected)
    {
        var success = Converters.UInt32.TryParse(input, out var result);

        Assert.True(success);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("")]
    [InlineData("4294967296")]
    [InlineData("-1")]
    public void TryParse_InvalidInput_ReturnsFalse(string input)
    {
        Assert.False(Converters.UInt32.TryParse(input, out _));
    }

    [Fact]
    public void RoundTrips() => TestHelpers.AssertRoundTrip(Converters.UInt32, 123456u);
}
