namespace RetroAdventureCreator.Infrastructure.Game.Models;

/// <summary>
/// Model for define scene in game
/// </summary>
public class SceneModel
{
    /// <summary>
    /// Unique Id in game for identify scene
    /// </summary>
    public string Id { get; init; } = default!;

    /// <summary>
    /// Scene description
    /// </summary>
    public MessageModel Description { get; init; } = default!;

    /// <summary>
    /// Scene dispatchers 
    /// </summary>
    public IEnumerable<DispatcherModel>? Dispatchers { get; init; }

    /// <summary>
    /// Objects in scene
    /// </summary>
    public IEnumerable<ObjectModel>? Objects { get; init; }
}
