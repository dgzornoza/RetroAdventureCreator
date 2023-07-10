using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Core.Serialization;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Interfaces;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Core.Services;


internal class GameSerializerService
{
    public byte[] Serialize(GameModel gameModel)
    {
        var indexes = this.GenerateGameComponentsIndexes(gameModel);

        return null;
    }

    private GameComponentsIndexes GenerateGameComponentsIndexes(GameModel gameModel) => new(
        Commands: GenerateComponentKeys(gameModel.Assets.Commands.SortByKey()),
        CommandsGroups: GenerateComponentKeys(gameModel.Assets.CommandsGroups.SortByKey()),
        Dispatchers: GenerateComponentKeys(gameModel.Assets.Dispatchers.SortByKey()),
        Flags: GenerateComponentKeys(gameModel.Flags.SortByKey()),
        InputCommands: GenerateComponentKeys(gameModel.Assets.InputCommands.SortByKey()),
        Messages: GenerateComponentKeys(gameModel.Assets.Messages.SortByKey()),
        Objects: GenerateComponentKeys(gameModel.Assets.Objects.SortByKey()),
        Scenes: GenerateComponentKeys(gameModel.Assets.Scenes.SortByKey()),
        VocabularyNouns: GenerateComponentKeys(gameModel.Assets.Vocabulary.SortByKey().Where(item => item.WordType == WordType.Noun)),
        VocabularyVerbs: GenerateComponentKeys(gameModel.Assets.Vocabulary.SortByKey().Where(item => item.WordType == WordType.Verb))
    );

    private IEnumerable<GameComponentKeyModel> GenerateComponentKeys(IEnumerable<IUniqueKey> objects) =>
        objects.Select((item, index) => new GameComponentKeyModel(item.Code, index));
}
