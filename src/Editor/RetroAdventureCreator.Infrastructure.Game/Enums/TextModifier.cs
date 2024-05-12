
namespace RetroAdventureCreator.Infrastructure.Game.Enums;

/// <summary>
/// Enum with text modifiers
/// </summary>
/// <remarks>All Enums start with 1 because 0 is reserved for end of objects serialization</remarks>
public enum TextModifier
{
    /// <summary>
    /// Change charset in text string
    /// </summary>
    /// <example>$"Current charset {{TextModifier.CharSet}} {{2}} Text with charset 2"</example>
    CharSet = 1,

    /// <summary>
    /// Set foreground text color, <see cref="Color"/>
    /// </summary>
    /// <example>$"Current color {{TextModifier.Color}} {{Color.Red}} Text with red color"</example>
    Color = 2,

    /// <summary>
    /// Set background text color
    /// </summary>
    /// <example>$"Current background color {{TextModifier.BackgroundColor}} {{Color.Blue}} Text with blue background color"</example>
    BackgroundColor = 3,

    /// <summary>
    /// Set Flash effect in text
    /// </summary>
    /// <example>$"Normal text {{TextModifier.FlashEffect}} Text with flash effect"</example>
    FlashEffect = 4,

    /// <summary>
    /// Invert foreground/background colors in text
    /// </summary>
    /// <example>$"Current colors {{TextModifier.InvertColor}} Text with inverted colors"</example>
    InvertColor = 5,
}
