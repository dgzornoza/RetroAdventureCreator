using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroAdventureCreator.Infrastructure.Game.Models;

public class AssetsModel
{
    /// <summary>
    /// Ccenes
    /// </summary>
    public IEnumerable<SceneModel> Scenes { get; init; } = default!;

    /// <summary>
    /// Vocabularies
    /// </summary>
    public IEnumerable<VocabularyModel> Vocabulary { get; init; } = default!;

    /// <summary>
    /// Messages
    /// </summary>
    public IEnumerable<MessageModel> Messages { get; init; } = default!;

    /// <summary>
    /// Objects
    /// </summary>
    public IEnumerable<ObjectModel> Objects { get; init; } = default!;

    /// <summary>
    /// Input commands
    /// </summary>
    public IEnumerable<InputCommandModel> InputCommands { get; init; } = default!;

    /// <summary>
    /// Commands
    /// </summary>
    public IEnumerable<CommandModel> Commands { get; init; } = default!;

    /// <summary>
    /// Commands groups
    /// </summary>
    public IEnumerable<CommandGroupModel> CommandsGroups { get; init; } = default!;

    /// <summary>
    /// Dispatcher commands
    /// </summary>
    public IEnumerable<DispatcherModel> Dispatchers { get; init; } = default!;
}
