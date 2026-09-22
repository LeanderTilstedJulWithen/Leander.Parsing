namespace Leander.Parsing.Tests;

public class GuidConverterTests
{
    [Fact]
    public void TryParse_ValidInput_ReturnsExpected()
    {
        var guid = Guid.NewGuid();

        var success = Converters.Guid.TryParse(guid.ToString(), out var result);

        Assert.True(success);
        Assert.Equal(guid, result);
    }

    [Theory]
    [InlineData("")]
    [InlineData("not-a-guid")]
    public void TryParse_InvalidInput_ReturnsFalse(string input)
    {
        Assert.False(Converters.Guid.TryParse(input, out _));
    }

    [Fact]
    public void RoundTrips() => TestHelpers.AssertRoundTrip(Converters.Guid, Guid.NewGuid());
}
