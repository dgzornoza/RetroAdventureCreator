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

/// <summary>
/// Service to serialize a game model to a byte array.
/// </summary>
internal class GameSerializerService
{
    public byte[] Serialize(GameModel gameModel)
    {
        var serializerBuilder = new SerializerFactory(gameModel);

        var commandGroupSerializer = serializerBuilder.Serialize<CommandGroupSerializer>();
        var commandsSerializer = serializerBuilder.Serialize<CommandsSerializer>();
        var afterInputCommandDispatchersSerializer = serializerBuilder.Serialize<AfterInputCommandDispatchersSerializer>();
        var beforeInputCommandDispatchersSerializer = serializerBuilder.Serialize<BeforeInputCommandDispatchersSerializer>();
        var flagsSerializer = serializerBuilder.Serialize<FlagsSerializer>();
        var inputCommandsSerializer = serializerBuilder.Serialize<InputCommandsSerializer>();
        var messagesSerializer = serializerBuilder.Serialize<MessagesSerializer>();
        var objectsSerializer = serializerBuilder.Serialize<ObjectsSerializer>();
        var playerSerializer = serializerBuilder.Serialize<PlayerSerializer>();
        var scenesSerializer = serializerBuilder.Serialize<ScenesSerializer>();
        var settingsSerializer = serializerBuilder.Serialize<SettingsSerializer>();
        var vocabularyNounsSerializer = serializerBuilder.Serialize<VocabularyNounsSerializer>();
        var vocabularyVerbsSerializer = serializerBuilder.Serialize<VocabularyVerbsSerializer>();

        return null;
    }
}
