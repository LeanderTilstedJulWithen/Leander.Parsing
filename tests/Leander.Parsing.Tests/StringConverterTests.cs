namespace Leander.Parsing.Tests;

public class StringConverterTests
{
    [Theory]
    [InlineData("")]
    [InlineData("hello")]
    [InlineData("  spaced  ")]
    public void TryParse_ReturnsInputUnchanged(string input)
    {
        var success = Converters.String.TryParse(input, out var result);

        Assert.True(success);
        Assert.Equal(input, result);
    }

    [Fact]
    public void RoundTrips() => TestHelpers.AssertRoundTrip(Converters.String, "hello world");
}
