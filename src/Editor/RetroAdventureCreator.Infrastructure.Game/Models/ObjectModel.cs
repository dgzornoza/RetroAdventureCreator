using RetroAdventureCreator.Infrastructure.Game.Enums;

namespace RetroAdventureCreator.Infrastructure.Game.Models;

/// <summary>
/// Model for define object in game
/// </summary>
public class ObjectModel
{
    /// <summary>
    /// Unique Id in game for identify object
    /// </summary>
    public string Id { get; init; } = default!;

    /// <summary>
    /// Vocabulary with object name
    /// </summary>
    public VocabularyModel Name { get; init; } = default!;

    /// <summary>
    /// Object description
    /// </summary>
    public MessageModel Description { get; init; } = default!;

    /// <summary>
    /// Object Weight from 0 to 31
    /// </summary>
    public int Weight { get; init; } = 0;

    /// <summary>
    /// Object Health from 0 to 7
    /// </summary>
    public int Health { get; init; } = 7;

    /// <summary>
    /// Object properties
    /// </summary>
    public ObjectProperties Properties { get; init; }

    /// <summary>
    /// Child Objects (only if has property ObjectProperties.Container)
    /// </summary>
    public IEnumerable<ObjectModel>? ChildObjects { get; init; }

    /// <summary>
    /// Required Complements Objects for use object
    /// </summary>
    public IEnumerable<ObjectModel>? RequiredComplements { get; init; }

    /// <summary>
    /// Current Complements Objects
    /// </summary>
    public IEnumerable<ObjectModel>? Complements { get; init; }

}
