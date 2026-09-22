namespace Leander.Parsing.Tests;

public class DateTimeConverterTests
{
    [Fact]
    public void Utc_TryParse_DateOnly_AssumesMidnightUtc()
    {
        var success = Converters.DateTimeUtc.TryParse("2024-01-01", out var result);

        Assert.True(success);
        Assert.Equal(DateTimeKind.Utc, result.Kind);
        Assert.Equal(new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), result);
    }

    [Fact]
    public void Utc_TryParse_OffsetInput_ConvertsToUtc()
    {
        var success = Converters.DateTimeUtc.TryParse("2024-01-01T10:00:00+02:00", out var result);

        Assert.True(success);
        Assert.Equal(DateTimeKind.Utc, result.Kind);
        Assert.Equal(new DateTime(2024, 1, 1, 8, 0, 0, DateTimeKind.Utc), result);
    }

    [Fact]
    public void Utc_Format_AppendsZ()
    {
        var value = new DateTime(2024, 1, 1, 10, 0, 0, DateTimeKind.Utc);

        Assert.Equal("2024-01-01T10:00:00Z", Converters.DateTimeUtc.Format(value));
    }

    [Fact]
    public void Local_TryParse_NoOffset_AssumesLocalKind()
    {
        var success = Converters.DateTimeLocal.TryParse("2024-01-01T10:00:00", out var result);

        Assert.True(success);
        Assert.Equal(DateTimeKind.Local, result.Kind);
    }

    [Fact]
    public void Local_TryParse_OffsetInput_ConvertsToLocalKind()
    {
        var success = Converters.DateTimeLocal.TryParse("2024-01-01T10:00:00Z", out var result);

        Assert.True(success);
        Assert.Equal(DateTimeKind.Local, result.Kind);
    }

    [Theory]
    [InlineData("")]
    [InlineData("not-a-date")]
    [InlineData("01/02/2024")]
    public void TryParse_NonIsoInput_ReturnsFalse(string input)
    {
        Assert.False(Converters.DateTimeUtc.TryParse(input, out _));
    }

    [Fact]
    public void Utc_RoundTrips() =>
        TestHelpers.AssertRoundTrip(Converters.DateTimeUtc, new DateTime(2024, 6, 1, 12, 30, 0, DateTimeKind.Utc));
}
