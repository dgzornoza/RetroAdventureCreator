using RetroAdventureCreator.Infrastructure.Game.Interfaces;

namespace RetroAdventureCreator.Infrastructure.Game.Models;

/// <summary>
/// Model for define scene in game
/// </summary>
public class SceneModel : IUniqueKey
{
    /// <summary>
    /// Unique Code for identify Scene
    /// </summary>
    public string Code { get; init; } = default!;

    /// <summary>
    /// Scene description
    /// </summary>
    public MessageModel Description { get; init; } = default!;

    /// <summary>
    /// Scene after input command dispatchers 
    /// </summary>
    public IEnumerable<DispatcherModel>? AfterInputCommandDispatchers { get; init; }

    /// <summary>
    /// Scene before input command dispatchers 
    /// </summary>
    public IEnumerable<DispatcherModel>? BeforeInputCommandDispatchers { get; init; }

    /// <summary>
    /// Objects in scene
    /// </summary>
    public IEnumerable<ObjectModel>? Objects { get; init; }
}
