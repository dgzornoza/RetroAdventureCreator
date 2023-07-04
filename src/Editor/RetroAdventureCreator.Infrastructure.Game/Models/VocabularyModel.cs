using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Interfaces;

namespace RetroAdventureCreator.Infrastructure.Game.Models;

/// <summary>
/// Model for define vocabulary model in game
/// </summary>
public class VocabularyModel : IUniqueKey
{
    /// <summary>
    /// Unique Code for identify Vocabulary
    /// </summary>
    public string Code { get; init; } = default!;

    /// <summary>
    /// Word type in vocabulary
    /// </summary>
    public WordType WordType { get; init; }

    /// <summary>
    /// List of synonyms for vocabulary (First word is main vocabulary word)
    /// </summary>
    public IEnumerable<string> Synonyms { get; init; } = default!;
}
