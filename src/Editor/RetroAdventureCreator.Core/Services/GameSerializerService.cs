using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Helpers;
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
        var serializerBuilder = new SerializerFactory(gameModel);

        var commandGroupSerializer = serializerBuilder.GetSerializer<CommandGroupSerializer>().Serialize(serializerBuilder.GameComponentsPointersModel);
        var commandsSerializer = serializerBuilder.GetSerializer<CommandsSerializer>().Serialize(serializerBuilder.GameComponentsPointersModel);
        var afterInputCommandDispatchersSerializer = serializerBuilder.GetSerializer<AfterInputCommandDispatchersSerializer>().Serialize(serializerBuilder.GameComponentsPointersModel);
        var beforeInputCommandDispatchersSerializer = serializerBuilder.GetSerializer<BeforeInputCommandDispatchersSerializer>().Serialize(serializerBuilder.GameComponentsPointersModel);
        var flagsSerializer = serializerBuilder.GetSerializer<FlagsSerializer>().Serialize(serializerBuilder.GameComponentsPointersModel);
        var inputCommandsSerializer = serializerBuilder.GetSerializer<InputCommandsSerializer>().Serialize(serializerBuilder.GameComponentsPointersModel);
        var messagesSerializer = serializerBuilder.GetSerializer<MessagesSerializer>().Serialize(serializerBuilder.GameComponentsPointersModel);
        var objectsSerializer = serializerBuilder.GetSerializer<ObjectsSerializer>().Serialize(serializerBuilder.GameComponentsPointersModel);
        var playerSerializer = serializerBuilder.GetSerializer<PlayerSerializer>().Serialize(serializerBuilder.GameComponentsPointersModel);
        var scenesSerializer = serializerBuilder.GetSerializer<ScenesSerializer>().Serialize(serializerBuilder.GameComponentsPointersModel);
        var settingsSerializer = serializerBuilder.GetSerializer<SettingsSerializer>().Serialize(serializerBuilder.GameComponentsPointersModel);
        var vocabularyNounsSerializer = serializerBuilder.GetSerializer<VocabularyNounsSerializer>().Serialize(serializerBuilder.GameComponentsPointersModel);
        var vocabularyVerbsSerializer = serializerBuilder.GetSerializer<VocabularyVerbsSerializer>().Serialize(serializerBuilder.GameComponentsPointersModel);

        return null;
    }
}
