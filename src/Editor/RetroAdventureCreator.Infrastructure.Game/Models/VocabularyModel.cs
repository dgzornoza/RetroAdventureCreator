using RetroAdventureCreator.Infrastructure.Game.Enums;

namespace RetroAdventureCreator.Infrastructure.Game.Models;

/// <summary>
/// Model for define vocabulary model in game
/// </summary>
public class VocabularyModel
{
    /// <summary>
    /// Unique Id in game for identify object
    /// </summary>
    public string Id { get; init; } = default!;

    /// <summary>
    /// Word type in vocabulary
    /// </summary>
    public WordType WordType { get; init; }  

    /// <summary>
    /// List of synonyms for vocabulary (First word is main vocabulary word)
    /// </summary>
    public IEnumerable<string>? Synonyms { get; init; }
}
