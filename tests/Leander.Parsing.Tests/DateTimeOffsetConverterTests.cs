namespace Leander.Parsing.Tests;

public class DateTimeOffsetConverterTests
{
    [Fact]
    public void TryParse_DateOnly_AssumesUtcOffset()
    {
        var success = Converters.DateTimeOffset.TryParse("2024-01-01", out var result);

        Assert.True(success);
        Assert.Equal(TimeSpan.Zero, result.Offset);
    }

    [Fact]
    public void TryParse_ExplicitOffset_PreservesOffset()
    {
        var success = Converters.DateTimeOffset.TryParse("2024-01-01T10:00:00+02:00", out var result);

        Assert.True(success);
        Assert.Equal(TimeSpan.FromHours(2), result.Offset);
    }

    [Fact]
    public void RoundTrips() =>
        TestHelpers.AssertRoundTrip(Converters.DateTimeOffset, new DateTimeOffset(2024, 6, 1, 12, 30, 0, TimeSpan.FromHours(-5)));
}
