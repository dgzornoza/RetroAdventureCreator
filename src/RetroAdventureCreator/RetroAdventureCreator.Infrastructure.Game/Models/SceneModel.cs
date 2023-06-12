namespace RetroAdventureCreator.Infrastructure.Game.Models;

/// <summary>
/// Model for define scene in game
/// </summary>
public record SceneModel
{
    /// <summary>
    /// Unique Id in game for identify scene
    /// </summary>
    public string Id { get; init; } = default!;

    /// <summary>
    /// Scene description, can be use <see cref="TextModifier"/> for text description
    /// </summary>
    public string Description { get; init; } = default!;

    /// <summary>
    /// Scene links to other scenes 
    /// </summary>
    public IEnumerable<VocabularyModel>? Links { get; init; } = default!;

    /// <summary>
    /// Scene dispatchers 
    /// </summary>
    public IEnumerable<DispatcherModel>? Dispatchers { get; init; } = default!;

    /// <summary>
    /// Objects in scene
    /// </summary>
    public IEnumerable<ObjectModel>? Objects { get; init; } = default!;
}
