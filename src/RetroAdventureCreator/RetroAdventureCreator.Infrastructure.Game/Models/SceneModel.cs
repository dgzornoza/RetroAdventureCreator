namespace RetroAdventureCreator.Infrastructure.Game.Models;

/// <summary>
/// Model for define scene in game
/// </summary>
public record SceneModel
{
    /// <summary>
    /// Unique code for identify scene
    /// </summary>
    public string Code { get; init; } = default!;

    /// <summary>
    /// Scene description, can be use <see cref="TextModifier"/> for text description
    /// </summary>
    public string Description { get; init; } = default!;

    /// <summary>
    /// Scene links to other scenes, where 
    /// Key = Vocabulary Code
    /// Value = Scene Code
    /// </summary>
    public IDictionary<string, string> Link { get; init; } = default!;
}
