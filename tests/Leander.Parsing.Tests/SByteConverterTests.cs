namespace Leander.Parsing.Tests;

public class SByteConverterTests
{
    [Theory]
    [InlineData("0", (sbyte)0)]
    [InlineData("-128", sbyte.MinValue)]
    [InlineData("127", sbyte.MaxValue)]
    public void TryParse_ValidInput_ReturnsExpected(string input, sbyte expected)
    {
        var success = Converters.SByte.TryParse(input, out var result);

        Assert.True(success);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("")]
    [InlineData("128")]
    [InlineData("-129")]
    public void TryParse_InvalidInput_ReturnsFalse(string input)
    {
        Assert.False(Converters.SByte.TryParse(input, out _));
    }

    [Fact]
    public void RoundTrips() => TestHelpers.AssertRoundTrip(Converters.SByte, (sbyte)-42);
}
