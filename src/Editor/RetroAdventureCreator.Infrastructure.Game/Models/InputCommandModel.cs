using RetroAdventureCreator.Infrastructure.Game.Interfaces;

namespace RetroAdventureCreator.Infrastructure.Game.Models;

public class InputCommandModel : IUniqueKey
{
    /// <summary>
    /// Unique Code for identify InputCommand
    /// </summary>
    public string Code { get; init; } = default!;

    /// <summary>
    /// Verbs to recognize
    /// </summary>
    public VocabularyModel Verbs { get; init; } = default!;

    /// <summary>
    /// Nouns to recognize
    /// </summary>
    public VocabularyModel? Nouns { get; init; }
}
