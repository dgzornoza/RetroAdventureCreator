using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Test.Infrastructure.Builders;

/// <summary>
/// Game builder
/// </summary>
/// <remarks>
///  Cretion Dependencies:
///
///     VocabularyModel
///     MessageModel
///     SettingsModel
///     CommandModel
///     ObjectModel -> MessageModel, VocabularyModel
///     InputCommandModel -> VocabularyModel
///     CommandGroupModel -> CommandModel
///     DispatcherModel -> InputCommandModel, CommandGroupModel
///     SceneModel -> MessageModel, DispatcherModel, ObjectModel
///     AssetsModel -> VocabularyModel, MessageModel, CommandModel, CommandGroupModel, InputCommandModel, DispatcherModel, ObjectModel, SceneModel
///     PlayerModel -> ObjectModel
///     GameModel -> PlayerModel, SettingsModel, AssetsModel
///     
///  Creation Order:
///     VocabularyModel
///     MessageModel
///     CommandModel
///     CommandGroupModel
///     InputCommandModel
///     DispatcherModel
///     ObjectModel
///     SceneModel
///     AssetsModel
///     PlayerModel
///     SettingsModel
///     GameModel
///     
/// </remarks>
abstract class GameBuilder
{
    protected PlayerModel Player { get; private set; }
    protected IDictionary<string, bool> Flags { get; private set; }
    protected SettingsModel Settings { get; private set; }
    protected AssetsModel Assets { get; private set; }

    protected IEnumerable<VocabularyModel> Vocabulary { get; private set; }
    protected IEnumerable<MessageModel> Messages { get; private set; }
    protected IEnumerable<CommandModel> Commands { get; private set; }
    protected IEnumerable<CommandGroupModel> CommandsGroups { get; private set; }
    protected IEnumerable<InputCommandModel> InputCommands { get; private set; }
    protected IEnumerable<DispatcherModel> Dispatchers { get; private set; }
    protected IEnumerable<ObjectModel> Objects { get; private set; }
    protected IEnumerable<SceneModel> Scenes { get; private set; }

    protected GameBuilder()
    {
        // Assets
        Vocabulary = CreateVocabulary();
        Messages = CreateMessages();
        Commands = CreateCommands();
        CommandsGroups = CreateCommandsGroups();
        InputCommands = CreateInputCommands();
        Dispatchers = CreateDispatchers();
        Objects = CreateObjects();
        Scenes = CreateScenes();

        // Game
        Assets = BuildAssets();
        Flags = CreateFlags();
        Player = CreatePlayer();
        Settings = CreateSettings();
    }

    public virtual GameModel BuildGame()
    {
        return new GameModel
        {
            Assets = Assets,
            Flags = Flags,
            Player = Player,
            Settings = Settings,
            MainSceneCode = MainSceneCode
        };
    }

    protected AssetsModel BuildAssets() => new()
    {
        Vocabulary = Vocabulary,
        Messages = Messages,
        Commands = Commands,
        CommandsGroups = CommandsGroups,
        InputCommands = InputCommands,
        Dispatchers = Dispatchers,
        Objects = Objects,
        Scenes = Scenes,
    };

    protected abstract string MainSceneCode { get; }

    protected abstract IDictionary<string, bool> CreateFlags();

    protected abstract PlayerModel CreatePlayer();

    protected abstract SettingsModel CreateSettings();

    protected abstract IEnumerable<MessageModel> CreateMessages();
    protected abstract IEnumerable<VocabularyModel> CreateVocabulary();
    protected abstract IEnumerable<ObjectModel> CreateObjects();
    protected abstract IEnumerable<CommandModel> CreateCommands();
    protected abstract IEnumerable<CommandGroupModel> CreateCommandsGroups();
    protected abstract IEnumerable<InputCommandModel> CreateInputCommands();
    protected abstract IEnumerable<DispatcherModel> CreateDispatchers();
    protected abstract IEnumerable<SceneModel> CreateScenes();
}

