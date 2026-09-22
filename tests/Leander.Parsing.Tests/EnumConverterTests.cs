namespace Leander.Parsing.Tests;

public class EnumConverterTests
{
    public enum Color { Red, Green, Blue }

    [Flags]
    private enum Permissions { None = 0, Read = 1, Write = 2, Execute = 4 }

    [Theory]
    [InlineData("Red", Color.Red)]
    [InlineData("red", Color.Red)]
    [InlineData("BLUE", Color.Blue)]
    public void TryParse_ValidMember_IsCaseInsensitive(string input, Color expected)
    {
        var success = Converters.Enum<Color>().TryParse(input, out var result);

        Assert.True(success);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("")]
    [InlineData("Purple")]
    [InlineData("99")]
    public void TryParse_UndefinedValue_ReturnsFalse(string input)
    {
        Assert.False(Converters.Enum<Color>().TryParse(input, out _));
    }

    [Fact]
    public void TryParse_FlagsCombination_Succeeds()
    {
        var success = Converters.Enum<Permissions>().TryParse("Read, Write", out var result);

        Assert.True(success);
        Assert.Equal(Permissions.Read | Permissions.Write, result);
    }

    [Fact]
    public void TryParse_FlagsWithUndefinedBit_ReturnsFalse()
    {
        Assert.False(Converters.Enum<Permissions>().TryParse("8", out _));
    }

    [Fact]
    public void RoundTrips() => TestHelpers.AssertRoundTrip(Converters.Enum<Color>(), Color.Green);
}
