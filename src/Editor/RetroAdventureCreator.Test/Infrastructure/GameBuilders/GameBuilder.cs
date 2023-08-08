using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Core.Services;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;
using RetroAdventureCreator.Test.Helpers;

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
///     PlayerModel -> ObjectModel
///     GameModel -> PlayerModel, SettingsModel, VocabularyModel, MessageModel, CommandModel, CommandGroupModel, InputCommandModel, DispatcherModel, ObjectModel, SceneModel
///     
/// </remarks>
public abstract class GameBuilder
{
    private GameModel? game;

    protected PlayerModel Player { get; private set; }    
    protected SettingsModel Settings { get; private set; }
    
    protected IEnumerable<VocabularyModel> Vocabulary { get; private set; }
    protected IEnumerable<MessageModel> Messages { get; private set; }
    protected IEnumerable<CommandModel> Commands { get; private set; }
    protected IEnumerable<CommandGroupModel> CommandsGroups { get; private set; }
    protected IEnumerable<InputCommandModel> InputCommands { get; private set; }
    protected IEnumerable<DispatcherModel> Dispatchers { get; private set; }
    protected IEnumerable<ObjectModel> Objects { get; private set; }
    protected IEnumerable<SceneModel> Scenes { get; private set; }
    protected IEnumerable<FlagModel> Flags { get; private set; }

    protected GameBuilder()
    {
        Vocabulary = CreateVocabulary();
        Messages = CreateMessages();
        Commands = CreateCommands();
        CommandsGroups = CreateCommandsGroups();
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
            
            Player = Player,
            Settings = Settings,
            
            Vocabulary = Vocabulary,
            Messages = Messages,
            Commands = Commands,
            CommandsGroups = CommandsGroups,
            InputCommands = InputCommands,
            Dispatchers = Dispatchers,
            Objects = Objects,
            Scenes = Scenes,
            Flags = Flags,
        };

        return game;
    }

    protected abstract string MainSceneCode { get; }
    
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
    protected abstract IEnumerable<FlagModel> CreateFlags();
}

