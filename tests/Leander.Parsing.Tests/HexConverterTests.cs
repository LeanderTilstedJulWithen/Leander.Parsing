namespace Leander.Parsing.Tests;

public class HexConverterTests
{
    [Theory]
    [InlineData("1A", 26)]
    [InlineData("0x1A", 26)]
    [InlineData("0X1a", 26)]
    public void Int32Hex_TryParse_ValidInput_ReturnsExpected(string input, int expected)
    {
        var success = Converters.Int32Hex.TryParse(input, out var result);

        Assert.True(success);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Int32Hex_TryParse_FullWidthHex_InterpretsAsTwosComplement()
    {
        var success = Converters.Int32Hex.TryParse("FFFFFFFF", out var result);

        Assert.True(success);
        Assert.Equal(-1, result);
    }

    [Fact]
    public void Int32Hex_Format_ProducesPrefixedUppercaseHex()
    {
        Assert.Equal("0x1A", Converters.Int32Hex.Format(26));
    }

    [Fact]
    public void Int32Hex_RoundTrips() => TestHelpers.AssertRoundTrip(Converters.Int32Hex, 4096);

    [Theory]
    [InlineData("1A", 26u)]
    [InlineData("0x1A", 26u)]
    public void UInt32Hex_TryParse_ValidInput_ReturnsExpected(string input, uint expected)
    {
        var success = Converters.UInt32Hex.TryParse(input, out var result);

        Assert.True(success);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void UInt32Hex_RoundTrips() => TestHelpers.AssertRoundTrip(Converters.UInt32Hex, 4096u);
}
