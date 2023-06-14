using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroAdventureCreator.Infrastructure.Game.Models;

public class InputCommandModel
{
    /// <summary>
    /// Unique Id in game for identify object
    /// </summary>
    public string Id { get; init; } = default!;

    /// <summary>
    /// Verb to recognize
    /// </summary>
    public VocabularyModel Verb { get; init; } = default!;

    /// <summary>
    /// Noun list to recognize
    /// </summary>
    public IEnumerable<VocabularyModel> Nouns { get; init; } = default!;
}
