namespace RetroAdventureCreator.Infrastructure.Game.Enums;

/// <summary>
/// Flag enum for Object properties
/// </summary>
[Flags]
public enum ObjectProperties : byte
{
    None = 0x00,
    InVisible = 0x01,
    IsEnabled = 0x01 << 1,
    InUse = 0x01 << 2,
    Portable = 0x01 << 3,
    IsContainer = 0x01 << 4,
}
