namespace RetroAdventureCreator.Infrastructure.Game.Enums;

/// <summary>
/// Flag enum for Scene properties
/// </summary>
/// <remarks>All Enums start with 1 because 0 is reserved for end of objects serialization</remarks>
[Flags]
public enum SceneProperties
{
    None = 0x01,
    IsDark = 0x01 << 1,
}
