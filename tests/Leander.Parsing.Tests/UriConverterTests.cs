namespace Leander.Parsing.Tests;

public class UriConverterTests
{
    [Fact]
    public void TryParse_AbsoluteUri_Succeeds()
    {
        var success = Converters.Uri.TryParse("https://example.com/path", out var result);

        Assert.True(success);
        Assert.Equal("https://example.com/path", result.AbsoluteUri);
    }

    [Theory]
    [InlineData("")]
    [InlineData("/relative/path")]
    [InlineData("not a uri")]
    public void TryParse_NonAbsoluteInput_ReturnsFalse(string input)
    {
        Assert.False(Converters.Uri.TryParse(input, out _));
    }

    [Fact]
    public void RoundTrips() => TestHelpers.AssertRoundTrip(Converters.Uri, new Uri("https://example.com/a?b=c"));
}
