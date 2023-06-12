namespace RetroAdventureCreator.Infrastructure.Game.Models;

public record SettingsModel
{
    public int Charset { get; init; }

    public int Color { get; init; }

    public int BackgroundColor { get; init; }

    public int FlashEffect { get; init; }

    public int InvertColor { get; init; }

    public int Over { get; init; }

    public int Border { get; init; }
}

