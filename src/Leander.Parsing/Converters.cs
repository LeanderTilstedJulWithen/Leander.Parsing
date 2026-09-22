using System.Globalization;
using Leander.Parsing.Internal;

namespace Leander.Parsing;

public static class Converters
{
    public static IConverter<string> String { get; } = new StringConverter();

    public static IConverter<int> Int32 { get; } = new IntConverter();

    public static IConverter<double> Double { get; } = new DoubleConverter();

    public static IConverter<DateTime> DateTimeUtc { get; } = new DateTimeConverterBuilder()
        .AddFormat("yyyy-MM-ddTHH:mm:ssK")
        .AddFormat("yyyy-MM-dd")
        .WithStyles(DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal)
        .Build();

    public static IConverter<DateTime> DateTimeLocal { get; } = new DateTimeConverterBuilder()
        .AddFormat("yyyy-MM-ddTHH:mm:ssK")
        .AddFormat("yyyy-MM-dd")
        .WithStyles(DateTimeStyles.AssumeLocal)
        .Build();

    public static IConverter<DateTimeOffset> DateTimeOffset { get; } = new DateTimeOffsetConverterBuilder()
        .AddFormat("yyyy-MM-ddTHH:mm:ssK")
        .AddFormat("yyyy-MM-dd")
        .WithStyles(DateTimeStyles.AssumeUniversal)
        .Build();
}
