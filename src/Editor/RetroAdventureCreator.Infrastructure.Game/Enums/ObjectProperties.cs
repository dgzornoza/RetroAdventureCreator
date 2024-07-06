namespace RetroAdventureCreator.Infrastructure.Game.Enums;

/// <summary>
/// Flag enum for Object properties
/// </summary>
[Flags]
public enum ObjectProperties : byte
{
    None = 0x00,
    InVisible = 0x01,
    InUse = 0x01 << 1,
    IsContainer = 0x01 << 2,
}
