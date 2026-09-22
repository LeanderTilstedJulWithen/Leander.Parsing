namespace Leander.Parsing.Tests;

public class SingleConverterTests
{
    [Theory]
    [InlineData("0", 0f)]
    [InlineData("3.14", 3.14f)]
    [InlineData("-2.5", -2.5f)]
    public void TryParse_ValidInput_ReturnsExpected(string input, float expected)
    {
        var success = Converters.Single.TryParse(input, out var result);

        Assert.True(success);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("")]
    [InlineData("not a number")]
    [InlineData("1,000")]
    public void TryParse_InvalidInput_ReturnsFalse(string input)
    {
        Assert.False(Converters.Single.TryParse(input, out _));
    }

    [Fact]
    public void RoundTrips() => TestHelpers.AssertRoundTrip(Converters.Single, 3.5f);
}
