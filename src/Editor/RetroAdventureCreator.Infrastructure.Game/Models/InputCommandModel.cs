using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Infrastructure.Game.Interfaces;

namespace RetroAdventureCreator.Infrastructure.Game.Models;

public class InputCommandModel : IUniqueKey
{
    /// <summary>
    /// Unique Code for identify InputCommand
    /// </summary>
    public string Code { get; init; } = default!;

    /// <summary>
    /// Verb to recognize
    /// </summary>
    public VocabularyModel Verb { get; init; } = default!;

    /// <summary>
    /// Noun list to recognize
    /// </summary>
    public IEnumerable<VocabularyModel>? Nouns { get; init; }
}
