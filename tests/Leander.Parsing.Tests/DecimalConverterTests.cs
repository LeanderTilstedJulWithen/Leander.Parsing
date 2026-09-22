using System.Globalization;

namespace Leander.Parsing.Tests;

public class DecimalConverterTests
{
    [Theory]
    [InlineData("0", "0")]
    [InlineData("3.14", "3.14")]
    [InlineData("-2.5", "-2.5")]
    public void TryParse_ValidInput_ReturnsExpected(string input, string expectedText)
    {
        var success = Converters.Decimal.TryParse(input, out var result);

        Assert.True(success);
        Assert.Equal(decimal.Parse(expectedText, CultureInfo.InvariantCulture), result);
    }

    [Theory]
    [InlineData("")]
    [InlineData("not a number")]
    [InlineData("1,000")]
    public void TryParse_InvalidInput_ReturnsFalse(string input)
    {
        Assert.False(Converters.Decimal.TryParse(input, out _));
    }

    [Fact]
    public void RoundTrips() => TestHelpers.AssertRoundTrip(Converters.Decimal, 19.99m);
}
