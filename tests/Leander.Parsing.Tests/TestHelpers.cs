namespace Leander.Parsing.Tests;

internal static class TestHelpers
{
    public static void AssertRoundTrip<T>(IConverter<T> converter, T value)
    {
        var formatted = converter.Format(value);
        var success = converter.TryParse(formatted, out var result);

        Assert.True(success, $"Expected '{formatted}' to parse back successfully.");
        Assert.Equal(value, result);
    }
}
