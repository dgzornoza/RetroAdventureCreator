using RetroAdventureCreator.Infrastructure.Game.Enums;

namespace RetroAdventureCreator.Infrastructure.Game.Models;

/// <summary>
/// Model for define settings model in game
/// </summary>
public class SettingsModel
{
    public byte Charset { get; init; }

    public Color Color { get; init; }

    public Color BackgroundColor { get; init; }

    public Color BorderColor { get; init; }
}

