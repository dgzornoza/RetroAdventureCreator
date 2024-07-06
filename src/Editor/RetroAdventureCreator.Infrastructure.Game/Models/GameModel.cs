namespace RetroAdventureCreator.Infrastructure.Game.Models;

/// <summary>
/// Main Game model
/// </summary>
public class GameModel
{
    /// <summary>
    /// Main game scene
    /// </summary>
    public string MainSceneCode { get; init; } = default!;

    /// <summary>
    /// Game actors
    /// </summary>
    public IEnumerable<ActorModel> Actors { get; init; } = default!;

    /// <summary>
    /// Default Game Settings
    /// </summary>
    public SettingsModel Settings { get; init; } = default!;

    /// <summary>
    /// Scenes
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
    /// Dispatcher commands
    /// </summary>
    public IEnumerable<DispatcherModel> Dispatchers { get; init; } = default!;

    /// <summary>
    /// Game Flags
    /// </summary>
    public IEnumerable<FlagModel> Flags { get; init; } = default!;
}
