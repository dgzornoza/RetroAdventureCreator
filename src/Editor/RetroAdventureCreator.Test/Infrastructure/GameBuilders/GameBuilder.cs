using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Test.Infrastructure.Builders;

abstract class GameBuilder
{
    protected PlayerModel Player { get; private set; }
    protected IDictionary<string, bool> Flags { get; private set; }
    protected SettingsModel Settings { get; private set; }
    protected AssetsModel Assets { get; private set; }

    protected IEnumerable<VocabularyModel> Vocabulary { get; private set; }
    protected IEnumerable<MessageModel> Messages { get; private set; }
    protected IEnumerable<CommandModel> Commands { get; private set; }
    protected IEnumerable<InputCommandModel> InputCommands { get; private set; }
    protected IEnumerable<DispatcherModel> Dispatchers { get; private set; }
    protected IEnumerable<ObjectModel> Objects { get; private set; }
    protected IEnumerable<SceneModel> Scenes { get; private set; }

    protected GameBuilder()
    {
        Vocabulary = CreateVocabulary();
        Messages = CreateMessages();
        Commands = CreateCommands();
        InputCommands = CreateInputCommands();
        Dispatchers = CreateDispatchers();
        Objects = CreateObjects();
        Scenes = CreateScenes();

        Assets = BuildAssets();
        Flags = BuildFlags();
        Player = BuildPlayer();
        Settings = BuildSettings();
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

    protected virtual AssetsModel BuildAssets() => new()
    {
        Vocabulary = CreateVocabulary(),
        Messages = CreateMessages(),
        Commands = CreateCommands(),
        InputCommands = CreateInputCommands(),
        Dispatchers = CreateDispatchers(),
        Objects = CreateObjects(),
        Scenes = CreateScenes(),
    };

    protected abstract string MainSceneCode { get; }

    protected abstract IDictionary<string, bool> BuildFlags();

    protected abstract PlayerModel BuildPlayer();

    protected abstract SettingsModel BuildSettings();

    protected abstract IEnumerable<MessageModel> CreateMessages();
    protected abstract IEnumerable<VocabularyModel> CreateVocabulary();
    protected abstract IEnumerable<ObjectModel> CreateObjects();
    protected abstract IEnumerable<CommandModel> CreateCommands();
    protected abstract IEnumerable<InputCommandModel> CreateInputCommands();
    protected abstract IEnumerable<DispatcherModel> CreateDispatchers();
    protected abstract IEnumerable<SceneModel> CreateScenes();
}

