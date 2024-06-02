using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Test.Infrastructure.Builders;

/// <summary>
/// Game builder
/// </summary>
/// <remarks>
///  Creation Dependencies:
///
///     VocabularyModel
///     MessageModel
///     CommandModel
///     InputCommandModel -> VocabularyModel
///     DispatcherModel -> InputCommandModel, CommandGroupModel
///     ObjectModel -> MessageModel, VocabularyModel
///     SceneModel -> MessageModel, DispatcherModel, ObjectModel
///     FlagsModel
///     PlayerModel -> ObjectModel
///     SettingsModel
///     GameModel -> PlayerModel, SettingsModel, VocabularyModel, MessageModel, CommandModel, CommandGroupModel, InputCommandModel, DispatcherModel, ObjectModel, SceneModel
///     
/// </remarks>
public abstract class GameBuilder
{
    private GameModel? game;

    protected IEnumerable<VocabularyModel> Vocabulary { get; private set; }
    protected IEnumerable<MessageModel> Messages { get; private set; }
    protected IEnumerable<CommandModel> Commands { get; private set; }
    protected IEnumerable<InputCommandModel> InputCommands { get; private set; }
    protected IEnumerable<DispatcherModel> Dispatchers { get; private set; }
    protected IEnumerable<ObjectModel> Objects { get; private set; }
    protected IEnumerable<SceneModel> Scenes { get; private set; }
    protected IEnumerable<FlagModel> Flags { get; private set; }

    protected PlayerModel Player { get; private set; }
    protected SettingsModel Settings { get; private set; }

    protected GameBuilder()
    {
        // Creation order
        Vocabulary = CreateVocabulary();
        Messages = CreateMessages();
        Commands = CreateCommands();
        InputCommands = CreateInputCommands();
        Dispatchers = CreateDispatchers();
        Objects = CreateObjects();
        Scenes = CreateScenes();
        Flags = CreateFlags();

        Player = CreatePlayer();
        Settings = CreateSettings();
    }

    public virtual GameModel BuildGame()
    {
        game ??= new GameModel
        {
            MainSceneCode = MainSceneCode,

            Vocabulary = Vocabulary,
            Messages = Messages,
            Commands = Commands,
            InputCommands = InputCommands,
            Dispatchers = Dispatchers,
            Objects = Objects,
            Scenes = Scenes,
            Flags = Flags,

            Player = Player,
            Settings = Settings,
        };

        return game;
    }

    protected abstract string MainSceneCode { get; }

    protected abstract IEnumerable<VocabularyModel> CreateVocabulary();
    protected abstract IEnumerable<MessageModel> CreateMessages();
    protected abstract IEnumerable<CommandModel> CreateCommands();
    protected abstract IEnumerable<InputCommandModel> CreateInputCommands();
    protected abstract IEnumerable<DispatcherModel> CreateDispatchers();
    protected abstract IEnumerable<ObjectModel> CreateObjects();
    protected abstract IEnumerable<SceneModel> CreateScenes();
    protected abstract IEnumerable<FlagModel> CreateFlags();

    protected abstract PlayerModel CreatePlayer();
    protected abstract SettingsModel CreateSettings();
}

