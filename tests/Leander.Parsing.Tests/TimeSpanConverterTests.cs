namespace Leander.Parsing.Tests;

public class TimeSpanConverterTests
{
    [Fact]
    public void TryParse_HoursMinutesSeconds_ReturnsExpected()
    {
        var success = Converters.TimeSpan.TryParse("00:30:00", out var result);

        Assert.True(success);
        Assert.Equal(TimeSpan.FromMinutes(30), result);
    }

    [Fact]
    public void TryParse_WithDaysComponent_ReturnsExpected()
    {
        var success = Converters.TimeSpan.TryParse("1.02:03:04", out var result);

        Assert.True(success);
        Assert.Equal(new TimeSpan(1, 2, 3, 4), result);
    }

    [Theory]
    [InlineData("")]
    [InlineData("not-a-timespan")]
    public void TryParse_InvalidInput_ReturnsFalse(string input)
    {
        Assert.False(Converters.TimeSpan.TryParse(input, out _));
    }

    [Fact]
    public void RoundTrips() => TestHelpers.AssertRoundTrip(Converters.TimeSpan, new TimeSpan(1, 2, 3, 4));
}
