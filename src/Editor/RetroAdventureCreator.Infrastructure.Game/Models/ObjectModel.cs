using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Interfaces;

namespace RetroAdventureCreator.Infrastructure.Game.Models;

/// <summary>
/// Model for define object in game
/// </summary>
public class ObjectModel : IUniqueKey
{
    /// <summary>
    /// Unique Code for identify Object
    /// </summary>
    public string Code { get; init; } = default!;

    /// <summary>
    /// Vocabulary with object name
    /// </summary>
    public VocabularyModel Name { get; init; } = default!;

    /// <summary>
    /// Object description
    /// </summary>
    public MessageModel Description { get; init; } = default!;

    /// <summary>
    /// Object properties
    /// </summary>
    public ObjectProperties Properties { get; init; }

    /// <summary>
    /// Component owner
    /// </summary>
    public string OwnerCode { get; init; } = default!;
}

/// <summary>
/// Model for define comoplex object in game
/// </summary>
public class ComplexObjectModel : ObjectModel
{
    /// <summary>
    /// Object Weight 256 max
    /// </summary>
    public byte Weight { get; init; } = byte.MinValue;

    /// <summary>
    /// Object Health 256 max
    /// </summary>
    public byte Health { get; init; } = byte.MaxValue;
}
